using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.Data;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Repository;

public class RoleAndPermissionRepository : IRoleAndPermissionRepository
{

    private readonly PizzaShopDbContext _dbContext;

    public RoleAndPermissionRepository(PizzaShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<RoleViewModel> GetRoleAsync()
    {
        var roles = (from role in _dbContext.Roles
                     select new RoleViewModel
                     {
                         RoleId = role.RoleId,
                         RoleName = role.RoleName
                     }).ToList();
        return roles;
    }
}
