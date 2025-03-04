using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IRoleAndPermissionRepository
{
    List<RoleViewModel> GetRoleAsync();
    List<PermissionTypeViewModel> PermissionByRoleAsync(int roleId);
    string GetRoleByRoleIdAsync(int roleId);
    Task<bool> UpdatePermsissionAsync(List<PermissionTypeViewModel> list);
    List<PermissionTypeViewModel> GetAllPermissionsByRoleName(string RoleName);
}
