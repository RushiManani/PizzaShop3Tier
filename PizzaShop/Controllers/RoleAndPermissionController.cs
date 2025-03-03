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

    [HttpPost]
    public async Task<IActionResult> UpdatePermission(IEnumerable<PermissionTypeViewModel> model)
    {
        List<PermissionTypeViewModel> list = model.ToList();
        bool isUpdated = await _roleAndPermissionRepository.UpdatePermsissionAsync(list);
        if (isUpdated)
        {
            TempData["ToastrMessage"] = "Permissions Updated Successfully";
            TempData["ToastrType"] = "success";
            return RedirectToAction("Index", "AdminDash");
        }
        return View("RoleView");
    }

}
