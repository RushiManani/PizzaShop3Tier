using PizzaShop.DAL.Models;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IAdminDashRepository
{
    Task<MyProfileViewModel> GetUserProfileAsync(string email);

    Task UpdateUserProfileAsync(MyProfileViewModel model);
}
