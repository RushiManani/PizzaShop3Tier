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

    public List<PermissionTypeViewModel> PermissionByRoleAsync(int roleId)
    {
        var permissions = (from role in _dbContext.Roles
                           join permissionType in _dbContext.Permissiontypes on role.RoleId equals permissionType.RoleId
                           join permission in _dbContext.Permissions on permissionType.PermissionId equals permission.PermissionId
                           where permissionType.RoleId == roleId
                           select new PermissionTypeViewModel
                           {
                               roleName = role.RoleName,
                               permissionName = permission.PermissionName,
                               canView = permissionType.CanView,
                               canAddEdit = permissionType.CanAddEdit,
                               canDelete = permissionType.CanDelete
                           }).ToList();
        return permissions;

    }

    public string GetRoleByRoleIdAsync(int roleId)
    {
        var roles = _dbContext.Roles.SingleOrDefault(r=>r.RoleId==roleId);
        string roleName = roles.RoleName;
        return roleName;
    }
}
