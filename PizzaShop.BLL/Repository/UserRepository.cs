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

    public List<UserViewModel> GetUserAsync(int page = 1, int pageSize = 5)
    {
        List<UserViewModel> users = (from user in _dbContext.Users
                                     join role in _dbContext.Roles on user.RoleId equals role.RoleId
                                     where user.Isdeleted == false
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

        users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return users;
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
