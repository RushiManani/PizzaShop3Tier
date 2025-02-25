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
        var user = await _adminDashRepository.GetUserProfileAsync(ViewData["UserEmail"]?.ToString()!);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserProfile(MyProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _adminDashRepository.UpdateUserProfileAsync(model);
            return RedirectToAction("Index", "AdminDash");
        }
        return View("GetUserProfile");
    }
}
