using Microsoft.AspNetCore.Mvc;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.Controllers;

public class RoleAndPermissionController : Controller
{

    private readonly IRoleAndPermissionRepository _roleAndPermissionRepository;

    public RoleAndPermissionController(IRoleAndPermissionRepository roleAndPermissionRepository)
    {
        _roleAndPermissionRepository = roleAndPermissionRepository;
    }

    public IActionResult RoleView()
    {
        List<RoleViewModel> roles = _roleAndPermissionRepository.GetRoleAsync();
        return View(roles);
    }

    public IActionResult PermissionView(int roleId)
    {
        List<PermissionTypeViewModel> list = _roleAndPermissionRepository.PermissionByRoleAsync(roleId);
        ViewData["RoleName"] = _roleAndPermissionRepository.GetRoleByRoleIdAsync(roleId);
        return View(list);
    }

    // [HttpPost]
    // public IActionResult UpdatePermission(List<PermissionTypeViewModel> model)
    // {

    // }

}
