using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Controllers;

public class RoleAndPermissionController : Controller
{
    public IActionResult RoleView()
    {
        return View();
    }
    
}
