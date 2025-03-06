using Microsoft.AspNetCore.Mvc;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.Controllers;

public class UserController : Controller
{

    private readonly IUserRepository _userRepository;
    private readonly IAdminDashRepository _adminDashRepository;
    private readonly IAuthRepository _authRepository;

    public UserController(IUserRepository userRepository, IAdminDashRepository adminDashRepository, IAuthRepository authRepository)
    {
        _userRepository = userRepository;
        _adminDashRepository = adminDashRepository;
        _authRepository = authRepository;
    }

    public IActionResult User_ListView(string? sortorder, string searchString, string currentFilter, int page = 1, int pageSize = 5)
    {

        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortorder) ? "name_desc" : "";
        ViewData["RoleSortParm"] = sortorder == "role" ? "role_desc" : "role";
        ViewData["CurrentSort"] = sortorder;
        ViewData["CurrentFilter"] = currentFilter;

        List<UserViewModel> users = _userRepository.GetUserAsync(page, pageSize);
        int totalItems = users.Count();
        int totalPages = (int)System.Math.Ceiling((double)totalItems / pageSize);
        ViewBag.TotalRecord = totalItems;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.PageSize = pageSize;
        ViewBag.StartCount = (page - 1) * pageSize + 1;
        ViewBag.EndCount = page * pageSize;

        page = page < 1 ? 1 : page;
        page = page > totalPages ? totalPages : page;

        if (!String.IsNullOrEmpty(searchString))
        {
            users = _userRepository.GetUserByNameAsync(searchString).ToList();
            users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        else
        {
            users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        switch (sortorder)
        {
            case "name_desc":
                users = users.OrderByDescending(u => u.UserName).ToList();
                break;
            case "role":
                users = users.OrderBy(u => u.RoleName).ToList();
                break;
            case "role_desc":
                users = users.OrderByDescending(u => u.RoleName).ToList();
                break;

            default:
                users = users.OrderBy(u => u.UserName).ToList();
                break;
        }

        return View(users);
    }

    [HttpPost]
    public IActionResult User_Delete(int id)
    {
        Console.WriteLine(id);
        if (id != 0)
        {
            _userRepository.DeleteUserAsync(id);
            TempData["ToastrMessage"] = "User Deleted Successfully";
            TempData["ToastrType"] = "success";
            return RedirectToAction("User_ListView");
        }

        return View("User_ListView");
    }

    public IActionResult User_AddView()
    {
        ViewBag.CountryList = _adminDashRepository.GetCountries().ToList();
        ViewBag.RoleList = _adminDashRepository.GetRoles().ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromForm] NewUserModel model)
    {
        if (ModelState.IsValid)
        {
            string subject = "Reset Your Password";
            string body = $"<p>Email : {model.Email}</p></br><p>Password : {model.Password}</p>;";
            model.Password = _authRepository.Encrypt(model.Password!);
            // model.CreatedBy = "Super Admin";
            bool isAdded = await _userRepository.AddUserAsync(model);
            if (isAdded)
            {
                await _authRepository.SendEmailAsync(model.Email!, subject, body);
                return RedirectToAction("User_ListView");
            }
            TempData["ToastrMessage"] = "Account Already Exists with this Email or UserName";
            TempData["ToastrType"] = "error";
            // return RedirectToAction("User_AddView");
        }
        Thread.Sleep(5000);
        return RedirectToAction("User_AddView");


    }

    [HttpGet]
    public async Task<IActionResult> User_EditView(int userID)
    {
        var user = await _userRepository.GetUserByIDAsync(userID);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUser(EditUserModel model)
    {
        if (ModelState.IsValid)
        {
            bool isupdated = await _userRepository.UpdateUserAsync(model);
            if (isupdated)
            {
                TempData["ToastrMessage"] = "User Updated Successfully";
                TempData["ToastrType"] = "success";
                return RedirectToAction("User_ListView", "User");
            }
            TempData["ToastrMessage"] = "Account Already Exists with this Username";
            TempData["ToastrType"] = "error";
            return RedirectToAction("User_EditView", new { userID = model.UserId });
        }
        return View("User_EditView");
    }

    [HttpGet]
    public async Task<IActionResult> GetStates(int countryid)
    {
        var states = _adminDashRepository.GetStates(countryid);
        ViewBag.StateList = states.ToList();
        return Json(states);
    }

    [HttpGet]
    public async Task<IActionResult> GetCities(int Stateid)
    {
        var cities = _adminDashRepository.GetCities(Stateid);
        ViewBag.CityList = cities.ToList();
        return Json(cities);
    }
}
