using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Controllers;

public class AdminDashController : Controller
{
    public IActionResult Index(){
        return View();
    }
}
