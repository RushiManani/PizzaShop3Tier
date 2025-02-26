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

    public UserRepository(PizzaShopDbContext dbContext,IAuthRepository authRepository)
    {
        _dbContext = dbContext;
        _authRepository = authRepository;
    }
    public List<UserViewModel> GetUserAsync(int page=1,int pageSize=5)
    {
        List<UserViewModel> users = (from user in _dbContext.Users join role in _dbContext.Roles on user.RoleId equals role.RoleId
                    where user.Isdeleted==false
                    select new UserViewModel
                    {
                        Email = user.Email,
                        Isactive = user.Isactive,
                        MobileNumber = user.MobileNumber,
                        RoleName = role.RoleName,
                        UserName = user.UserName,
                        UserId = user.UserId
                    }).ToList();
        
        users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
        return users;
    } 

    public async Task DeleteUserAsync(int userId)
    {
        var user = _dbContext.Users.FirstOrDefault(u=>u.UserId==userId);
        if(user!=null)
        {
            user.Isdeleted=true;
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task AddUserAsync(User model)
    {
        var role = _dbContext.Roles.FirstOrDefault(r=>r.RoleId==model.RoleId);
        if(role.RoleName=="Admin")
        {
            model.Isadmin=true;
        }
        await _dbContext.Users.AddAsync(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<NewUserModel?> GetUserByIDAsync(int userID)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u=>u.UserId==userID);
        var edituser = new NewUserModel{
            UserId=user.UserId,
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
            ProfilePicture = user.ProfilePicture,
            CountryList = await _dbContext.Countries.ToListAsync(),
            StateList = await _dbContext.States.Where(u=>u.CountryId==user.CountryId).ToListAsync(),
            CityList = await _dbContext.Cities.Where(u=>u.StateId==user.StateId).ToListAsync(),
            RoleList = await _dbContext.Roles.ToListAsync()
        };
        return edituser; 
    }

    public async Task UpdateUserAsync(NewUserModel model)
    {
        var users = _dbContext.Users.FirstOrDefault(u => u.UserId == model.UserId);
        users.FirstName = model.FirstName!;
        users.LastName = model.LastName;
        users.UserName = model.UserName!;
        users.RoleId = model.RoleId;
        users.Email = model.Email;
        users.Isactive = model.Isactive;
        users.MobileNumber = model.Phone;
        users.Address = model.Address;
        users.Zipcode = model.Zipcode;
        users.CountryId = model.CountryId;
        users.StateId = model.StateId;
        users.CityId = model.CityId;
        _dbContext.Update(users);
        await _dbContext.SaveChangesAsync();
    }
}
