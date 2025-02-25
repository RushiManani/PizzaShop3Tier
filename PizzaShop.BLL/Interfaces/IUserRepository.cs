using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IUserRepository
{
    List<UserViewModel> GetUserAsync(int page,int pageSize);
}
