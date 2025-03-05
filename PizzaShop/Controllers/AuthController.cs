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
                if (user.IsLoginFirstTime == true)
                {
                    TempData["Email"] = user.Email;
                    return RedirectToAction("User_ResetPassword");
                }
                else
                {
                    string role = await _authRepository.GetRoleAsync(user.RoleId);
                    var token = _jwtRepository.GenerateJwtToken(user.UserName, user.Email, role);
                    Response.Cookies.Append("JWT", token);
                    TempData["ToastrMessage"] = "Login Successfull";
                    TempData["ToastrType"] = "success";
                    return RedirectToAction("Index", "AdminDash");
                }
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

    public IActionResult User_ForgotPassword(string Email)
    {
        if(Email==null)
        {
            TempData["ToastrMessage"] = "Please enter Email Address";
            TempData["ToastrType"] = "error";
            return RedirectToAction("User_Login");
        }
        ViewData["Email"]=Email;
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
                string body = $@"
                <html>
                    <body style='background-color: #F8F9FA'>
                    <div style='background-color: #0067a9;display: flex;justify-content: center;align-items: center;height: 100;'>
                        <img src='https://i.ibb.co/9mk5kMWL/pizzashop-logo.png' alt='img here' height='50'>
                        <h1 style='color: white;margin-left: 20px;'>PIZZASHOP</h1>
                    </div>
                    <p>Pizza Shop,</p>
                    <p>Please click <a href='{callbackUrl}' style='color: #0067a9;'>here</a> for reset your account Password.</p>
                    <p>If you encounter any issues or have any question,please do not hesitate to contact our support team.</p>
                    <p><span style='color: #FFC107'>Important Note:</span>For security reasons,the link wil expire in 24 hours.If you
                        did not request a password reset, please ignore this email or contact our support team immediately.</p>
                    </body>

                </html>";
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
        return RedirectToAction("User_Login");
    }
}
