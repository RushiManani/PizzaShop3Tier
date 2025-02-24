using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.Data;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly PizzaShopDbContext _dbContext;

    public AuthRepository(PizzaShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public AuthViewModel Login(int userID)
    {
        AuthViewModel au = new AuthViewModel();
        return au;
    }

}
