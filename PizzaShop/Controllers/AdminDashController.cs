using Microsoft.AspNetCore.Mvc;
using PizzaShop.BLL.Interfaces;

namespace PizzaShop.Controllers;

public class AdminDashController : Controller
{
    private readonly IJWTRepository _jwtRepository;

    public AdminDashController(IJWTRepository jWTRepository)
    {
        _jwtRepository = jWTRepository;
    }

    public IActionResult Index()
    {
        List<string> jwtlist = _jwtRepository.ReadJWTToken();
        ViewData["UserEmail"] = jwtlist[0];
        ViewData["UserName"] = jwtlist[1];
        ViewData["RoleName"] = jwtlist[2];
        return View();
    }
}
