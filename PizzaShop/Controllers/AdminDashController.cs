using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.Models;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.Controllers;

[Authorize(Roles = "Admin")]
public class AdminDashController : Controller
{
    private readonly IJWTRepository _jwtRepository;
    private readonly IAdminDashRepository _adminDashRepository;

    public AdminDashController(IJWTRepository jWTRepository, IAdminDashRepository adminDashRepository)
    {
        _adminDashRepository = adminDashRepository;
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

    [HttpGet]
    public async Task<IActionResult> GetUserProfile()
    {
        List<string> jwtlist = _jwtRepository.ReadJWTToken();
        ViewData["UserEmail"] = jwtlist[0];
        ViewData["UserName"] = jwtlist[1];
        ViewData["RoleName"] = jwtlist[2];
        ViewData["ProfilePhoto"] = jwtlist[3];
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
        return View();
    }

    public IActionResult UpdatePassword(ChangePassword model)
    {
        if (ModelState.IsValid)
        {
            List<string> jwtlist = _jwtRepository.ReadJWTToken();
            var email = jwtlist[0];
            _adminDashRepository.UpdatePasswordAsync(email, model.CurrentPassword, model.NewPassword);
            TempData["ToastrMessage"] = "Password Changed Successfully";
            TempData["ToastrType"] = "success";
            return RedirectToAction("Index");
        }
        return RedirectToAction("User_Login","Auth");
    }
}
