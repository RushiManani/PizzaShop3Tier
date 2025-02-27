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

    public IActionResult PermissionView()
    {
        return View();
    }

}
