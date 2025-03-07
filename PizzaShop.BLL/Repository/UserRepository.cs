using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.Data;
using PizzaShop.DAL.Models;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Repository;

public class UserRepository : IUserRepository
{
    private readonly PizzaShopDbContext _dbContext;
    private readonly IAuthRepository _authRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UserRepository(PizzaShopDbContext dbContext, IAuthRepository authRepository, IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _authRepository = authRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<(List<UserViewModel> UserList, int Count, int PageSize, int PageNumber, string SortBy, string SortOrder, string SearchKey)> GetUserAsync(int PageSize, int PageNumber, string sortBy, string sortOrder, string SearchKey)
    {
        var userslist = from user in _dbContext.Users
                        join role in _dbContext.Roles on user.RoleId equals role.RoleId
                        where user.Isdeleted == false && (
                        user.UserName.ToLower().Contains(SearchKey))
                        select new UserViewModel
                        {
                            UserId = user.UserId,
                            Email = user.Email,
                            UserName = user.UserName,
                            MobileNumber = user.MobileNumber,
                            RoleName = role.RoleName,
                            Isactive = user.Isactive,
                            ProfilePicture = user.ProfilePicture,
                        };

        switch (sortBy)
        {
            case "name":
                userslist = (sortOrder == "asc") ? userslist.OrderBy(u => u.UserName) : userslist.OrderByDescending(u => u.UserName);
                break;
            case "role":
                userslist = (sortOrder == "asc") ? userslist.OrderBy(u => u.RoleName) : userslist.OrderByDescending(u => u.RoleName);
                break;
            default:
                userslist = userslist.OrderBy(u => u.UserName);
                break;
        }

        var count = await userslist.CountAsync();

        if (PageNumber < 1)
        {
            PageNumber = 1;
        }

        var totalPages = (int)Math.Ceiling((double)count / PageSize);

        if (PageNumber > totalPages)
        {
            PageNumber = totalPages;
        }

        if (PageNumber < 1)
        {
            PageNumber = 1;
        }

        var userList = await userslist.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();

        return (userList, count, PageSize, PageNumber, sortBy, sortOrder, SearchKey);
    }

    public List<UserViewModel> GetUserByNameAsync(string searchString)
    {
        List<UserViewModel> users = (from user in _dbContext.Users
                                     join role in _dbContext.Roles on user.RoleId equals role.RoleId
                                     where user.Isdeleted == false && user.UserName.ToLower().Contains(searchString.ToLower())
                                     select new UserViewModel
                                     {
                                         Email = user.Email,
                                         Isactive = user.Isactive,
                                         MobileNumber = user.MobileNumber,
                                         RoleName = role.RoleName,
                                         UserName = user.UserName,
                                         UserId = user.UserId,
                                         ProfilePicture = user.ProfilePicture
                                     }).ToList();
        return users;
    }

    public User GetUserByEmail(string email)
    {
        return _dbContext.Users.SingleOrDefault(u => u.Email == email);
    } 

    public async Task DeleteUserAsync(int userId)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
        if (user != null)
        {
            user.Isdeleted = true;
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> AddUserAsync(NewUserModel model)
    {
        try
        {
            var userExist = _dbContext.Users.SingleOrDefaultAsync(u => u.Email == model.Email || u.UserName == model.UserName);
            if (userExist.Result != null)
            {
                return false;
            }
            else
            {
                var role = _dbContext.Roles.FirstOrDefault(r => r.RoleId == model.RoleId);
                bool isAdmin = false;
                if (role!.RoleName == "Admin")
                {
                    isAdmin = true;
                }
                var user = new User
                {
                    UserName = model.UserName!,
                    FirstName = model.FirstName!,
                    LastName = model.LastName,
                    Email = model.Email!,
                    Password = model.Password!,
                    CountryId = model.CountryId,
                    StateId = model.StateId,
                    CityId = model.CityId,
                    RoleId = model.RoleId,
                    Address = model.Address,
                    MobileNumber = model.Phone,
                    ProfilePicture = await UploadPhotoAsync(model.ProfilePicture!),
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Super Admin",
                    Isadmin = isAdmin,
                    IsLoginFirstTime = true
                };

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }

    }

    public async Task<EditUserModel?> GetUserByIDAsync(int userID)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.UserId == userID);
        var edituser = new EditUserModel
        {
            UserId = user!.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            RoleId = user.RoleId,
            CountryId = user.CountryId,
            StateId = user.StateId,
            CityId = user.CityId,
            Email = user.Email,
            Address = user.Address,
            Phone = user.MobileNumber,
            Zipcode = user.Zipcode,
            Isactive = user.Isactive,
            CountryList = await _dbContext.Countries.ToListAsync(),
            StateList = await _dbContext.States.Where(u => u.CountryId == user.CountryId).ToListAsync(),
            CityList = await _dbContext.Cities.Where(u => u.StateId == user.StateId).ToListAsync(),
            RoleList = await _dbContext.Roles.ToListAsync()
        };
        return edituser;
    }

    public async Task<bool> UpdateUserAsync(EditUserModel model)
    {
        // var userExist = _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Isdeleted!=true && u.UserId!=model.UserId );
        // if (userExist != null)
        // {
        //     return false;
        // }
        // else
        // {
        var users = _dbContext.Users.FirstOrDefault(u => u.UserId == model.UserId);
        users!.FirstName = model.FirstName!;
        users.LastName = model.LastName;
        users.UserName = model.UserName!;
        users.RoleId = model.RoleId;
        users.Email = model.Email!;
        users.Isactive = model.Isactive;
        users.MobileNumber = model.Phone;
        users.Address = model.Address;
        users.Zipcode = model.Zipcode;
        users.CountryId = model.CountryId;
        users.StateId = model.StateId;
        users.CityId = model.CityId;
        users.ProfilePicture = await UploadPhotoAsync(model.ProfilePicture!);
        _dbContext.Update(users);
        await _dbContext.SaveChangesAsync();
        return true;
        // }
    }

    public async Task<string?> UploadPhotoAsync(IFormFile photo)
    {
        if (photo == null || photo.Length == 0)
            return null;

        string folder = "user/photos/";
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder, uniqueFileName);

        using (var fileStream = new FileStream(serverFolder, FileMode.Create))
        {
            await photo.CopyToAsync(fileStream);
        }

        return "/" + folder + uniqueFileName;
    }
}
