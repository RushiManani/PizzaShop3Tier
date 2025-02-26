using PizzaShop.DAL.Models;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IUserRepository
{
    List<UserViewModel> GetUserAsync(int page,int pageSize);
    Task DeleteUserAsync(int userId);
    Task AddUserAsync(User model);
    Task<NewUserModel?> GetUserByIDAsync(int userID);
    Task UpdateUserAsync(NewUserModel model);
}
