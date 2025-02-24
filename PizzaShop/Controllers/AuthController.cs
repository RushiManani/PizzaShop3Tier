using Microsoft.AspNetCore.Mvc;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.Controllers;

public class AuthController : Controller
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public IActionResult User_Login(){
        return View();
    }

    [HttpPost]
    public IActionResult Login(int userId)
    {
        try
        {
            AuthViewModel model = _authRepository.Login(userId);
            return View("User_Login",model);
        }
        catch
        {
            return View("User_Login");
        }
    }
}
