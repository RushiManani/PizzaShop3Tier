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

    // public async Task<AuthViewModel> LoginAsync(int userID)
    // {
    //     if(userID!=0){
    //         var users = await _dbContext.Users.FindAsync(userID);
    //         AuthViewModel model = new AuthViewModel{
    //             UserId = users.UserId,
    //             Email = users.Email,
    //             Password = users.Password
    //         };
    //         return model;
    //     }
    //     return null;
    // }
}
