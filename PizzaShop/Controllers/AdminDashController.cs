using Microsoft.AspNetCore.Mvc;
using PizzaShop.BLL.Helpers;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.Controllers;

[CustomAuthorize(new string[] { "Admin" })]
public class AdminDashController : Controller
{
    private readonly IJWTRepository _jwtRepository;
    private readonly IAdminDashRepository _adminDashRepository;

    public AdminDashController(IJWTRepository jWTRepository, IAdminDashRepository adminDashRepository)
    {
        _adminDashRepository = adminDashRepository;
        _jwtRepository = jWTRepository;
    }

    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Index()
    {
        List<string> jwtlist = _jwtRepository.ReadJWTToken();
        ViewData["UserEmail"] = jwtlist[0];
        ViewData["UserName"] = jwtlist[1];
        ViewData["RoleName"] = jwtlist[2];
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetUserProfile()
    {
        List<string> jwtlist = _jwtRepository.ReadJWTToken();
        ViewData["UserEmail"] = jwtlist[0];
        ViewData["UserName"] = jwtlist[1];
        ViewData["RoleName"] = jwtlist[2];
        var user = await _adminDashRepository.GetUserProfileAsync(ViewData["UserEmail"]?.ToString()!);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserProfile(MyProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _adminDashRepository.UpdateUserProfileAsync(model);
            TempData["ToastrMessage"] = "Profile Updated Successfully";
            TempData["ToastrType"] = "success";
            return RedirectToAction("Index", "AdminDash");
        }
        return View("GetUserProfile");
    }

    [HttpGet]
    public async Task<IActionResult> GetStates(int countryid)
    {
        var states = _adminDashRepository.GetStates(countryid);
        return Json(states);
    }

    [HttpGet]
    public async Task<IActionResult> GetCities(int Stateid)
    {
        var cities = _adminDashRepository.GetCities(Stateid);
        return Json(cities);
    }

    public IActionResult ChangePassword()
    {
        List<string> jwtlist = _jwtRepository.ReadJWTToken();
        ViewData["UserEmail"] = jwtlist[0];
        return View();
    }

    public async Task<IActionResult> UpdatePassword(ChangePassword model)
    {
        if (ModelState.IsValid)
        {
            List<string> jwtlist = _jwtRepository.ReadJWTToken();
            var email = jwtlist[0];
            bool isUpdated = await _adminDashRepository.UpdatePasswordAsync(email, model.CurrentPassword, model.NewPassword);
            if (isUpdated)
            {
                TempData["ToastrMessage"] = "Password Changed Successfully";
                TempData["ToastrType"] = "success";
                return RedirectToAction("User_Login", "Auth");
            }
            TempData["ToastrMessage"] = "Failed!, Current Password is Wrong";
            TempData["ToastrType"] = "error";
            return RedirectToAction("ChangePassword");

        }
        return RedirectToAction("User_Login", "Auth");
    }
}
