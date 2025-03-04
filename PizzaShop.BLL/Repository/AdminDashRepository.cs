using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.Data;
using PizzaShop.DAL.Models;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Repository;

public class AdminDashRepository : IAdminDashRepository
{

    private readonly PizzaShopDbContext _dbContext;
    private readonly IAuthRepository _authrepository;

    public AdminDashRepository(PizzaShopDbContext dbContext,IAuthRepository authRepository)
    {
        _dbContext = dbContext;
        _authrepository = authRepository;
    }

    public async Task<MyProfileViewModel> GetUserProfileAsync(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        var profile = new MyProfileViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            MobileNumber = user.MobileNumber,
            CountryId = user.CountryId,
            StateId = user.StateId,
            CityId = user.CityId,
            Address = user.Address,
            ProfilePicture = user.ProfilePicture,
            Email = user.Email,
            UserId = user.UserId,
            Zipcode = user.Zipcode,
            CountryList = await _dbContext.Countries.ToListAsync(),
            StateList = await _dbContext.States.Where(u=>u.CountryId==user.CountryId).ToListAsync(),
            CityList = await _dbContext.Cities.Where(u=>u.StateId==user.StateId).ToListAsync()
        };
        return profile;
    }

    public async Task UpdateUserProfileAsync(MyProfileViewModel model)
    {
        var users = _dbContext.Users.FirstOrDefault(u => u.Email == model.Email);
        users.FirstName = model.FirstName!;
        users.LastName = model.LastName;
        users.UserName = model.UserName!;
        users.MobileNumber = model.MobileNumber;
        users.Address = model.Address;
        users.Zipcode = model.Zipcode;
        users.CountryId = model.CountryId;
        users.StateId = model.StateId;
        users.CityId = model.CityId;
        _dbContext.Update(users);
        await _dbContext.SaveChangesAsync();
    }

    public List<Country> GetCountries()
    {
        return _dbContext.Countries.ToList();
    }

    public List<Role> GetRoles()
    {
        return _dbContext.Roles.ToList();
    }

    public List<State> GetStates(int CountryId)
    {
        return _dbContext.States.Where(s=>s.CountryId==CountryId).ToList();
    }

    public List<City> GetCities(int StateId)
    {
        return _dbContext.Cities.Where(c=>c.StateId==StateId).ToList();
    }

    public async Task<bool> UpdatePasswordAsync(string email,string currentPassword,string newPassword)
    {
        var users = _dbContext.Users.FirstOrDefault(u=>u.Email==email);
        if(users!=null)
        {
            if(users.Password==_authrepository.Encrypt(currentPassword))
            {
                string newPasswordHash = _authrepository.Encrypt(newPassword);
                users.Password = newPasswordHash;
                _dbContext.Update(users);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        return false;
    }
}
