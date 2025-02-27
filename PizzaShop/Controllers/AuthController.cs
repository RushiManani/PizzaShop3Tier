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
                TempData["ToastrMessage"] = "Login Successfull";
                TempData["ToastrType"] = "success";
                return RedirectToAction("Index", "AdminDash");
            }
            TempData["ToastrMessage"] = "Invalid Username or Password";
            TempData["ToastrType"] = "error";
            return View("User_Login");
        }
        catch
        {
            return View("User_Login");
        }
    }

    public IActionResult User_ForgotPassword()
    {
        return View();
    }

    public async Task<IActionResult> ForgotPassword(ForgotPassword model)
    {
        try
        {
            var user = await _authRepository.ForgotPasswordAsync(model.Email!);
            if (user != null)
            {
                TempData["Email"] = user.Email;
                var callbackUrl = Url.ActionLink(
                    "User_ResetPassword", "Auth"
                );
                string subject = "Reset Your Password";
                string body = $"<a href='{callbackUrl}'>click  here</a> to reset password.;";
                Task<bool> linkReset = _authRepository.SendEmailAsync(user.Email, subject, body);
                TempData["ToastrMessage"] = "Email sent Successfully, Check your Mailbox";
                TempData["ToastrType"] = "success";
                return View("User_ForgotPassword");
            }
            TempData["ToastrMessage"] = "Incorrect Email, Email not Found";
            TempData["ToastrType"] = "error";
            return View("User_ForgotPassword");
        }
        catch
        {
            return View("User_ForgotPassword");
        }
    }

    public IActionResult User_ResetPassword()
    {
        return View();
    }

    public IActionResult ResetPassword(ResetPassword model)
    {
        if (ModelState.IsValid)
        {
            if (model.NewPassword == model.ConfirmPassword)
            {
                string email = TempData["Email"]!.ToString()!;
                _authRepository.ResetPasswordAsync(email, model.NewPassword);
                TempData["ToastrMessage"] = "Reset Password Successfully";
                TempData["ToastrType"] = "success";
            }
        }
        return View("User_Login");
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("JWT");
        Response.Cookies.Delete("UserEmail");
        TempData["ToastrMessage"] = "Logout Successfully";
        TempData["ToastrType"] = "success";
        return View("User_Login");
    }
}
