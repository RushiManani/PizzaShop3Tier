using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.Data;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Repository;

public class UserRepository : IUserRepository
{
    private readonly PizzaShopDbContext _dbContext;

    public UserRepository(PizzaShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<UserViewModel> GetUserAsync(int page=1,int pageSize=5)
    {
        List<UserViewModel> users = (from user in _dbContext.Users join role in _dbContext.Roles on user.RoleId equals role.RoleId
                    select new UserViewModel
                    {
                        Email = user.Email,
                        Isactive = user.Isactive,
                        MobileNumber = user.MobileNumber,
                        RoleName = role.RoleName,
                        UserName = user.UserName,
                    }).ToList();
        
        users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
        return users;
    } 
}
