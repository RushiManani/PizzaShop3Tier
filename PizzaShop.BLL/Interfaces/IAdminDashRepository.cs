using PizzaShop.DAL.Models;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IAdminDashRepository
{
    Task<MyProfileViewModel> GetUserProfileAsync(string email);

    Task UpdateUserProfileAsync(MyProfileViewModel model);

    List<Role> GetRoles();
    List<Country> GetCountries();
    List<State> GetStates(int countryId);
    List<City> GetCities(int stateId);
    Task<bool> UpdatePasswordAsync(string email, string currentPassword, string newPassword);
}
