using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IAuthRepository
{
    AuthViewModel Login(int UserId);
}
