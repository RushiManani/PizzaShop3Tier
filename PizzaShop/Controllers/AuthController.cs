using Microsoft.AspNetCore.Mvc;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.Controllers;

public class AuthController : Controller
{
    private readonly IAuthRepository _authRepository;
    private readonly IJWTRepository _jwtRepository;

    public AuthController(IAuthRepository authRepository, IJWTRepository jWTRepository)
    {
        _authRepository = authRepository;
        _jwtRepository = jWTRepository;
    }

    public IActionResult User_Login()
    {
        string req_cookie = Request.Cookies["UserEmail"]!;
        if (!string.IsNullOrEmpty(req_cookie))
        {
            return RedirectToAction("Index", "AdminDash");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(AuthViewModel model)
    {
        try
        {
            var user = await _authRepository.LoginAsync(model.Email!, model.Password!, model.RememberMe);
            if (user != null)
            {
                string role = await _authRepository.GetRoleAsync(user.RoleId);
                var token = _jwtRepository.GenerateJwtToken(user.UserName, user.Email, role);
                Response.Cookies.Append("JWT", token);
                return RedirectToAction("Index", "AdminDash");
            }
            return View("User_Login");
        }
        catch
        {
            return View("User_Login");
        }
    }

    public IActionResult User_ForgotPassword(){
        return View();
    }

    public async Task<IActionResult> ForgotPassword(AuthViewModel model)
    {
        try{
            var user = await _authRepository.ForgotPasswordAsync(model.Email!);
            if(user!=null)
            {
                return View("User_ForgotPassword");
            }
            return View("User_ForgotPassword");
        }
        catch{
            return View("User_ForgotPassword");
        }
    }

    public IActionResult User_ResetPassword(){
        return View();
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("JWT");
        Response.Cookies.Delete("UserEmail");
        return View("User_Login");
    }
}
