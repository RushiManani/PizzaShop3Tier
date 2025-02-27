using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IRoleAndPermissionRepository
{
    List<RoleViewModel> GetRoleAsync();
}
