using Microsoft.AspNetCore.Mvc;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.Controllers;

public class UserController : Controller
{

    private readonly IUserRepository _userRepository;
    private readonly IAdminDashRepository _adminDashRepository;
    private readonly IAuthRepository _authRepository;
    private readonly IJWTRepository _jwtRepository;

    public UserController(IUserRepository userRepository, IAdminDashRepository adminDashRepository, IAuthRepository authRepository,IJWTRepository jWTRepository)
    {
        _userRepository = userRepository;
        _adminDashRepository = adminDashRepository;
        _authRepository = authRepository;
        _jwtRepository = jWTRepository;
    } 

    [HttpGet]
    public async Task<IActionResult> User_ListView(int PageSize = 5, int PageNumber = 1, string sortBy = "name", string sortOrder = "asc", string SearchKey = "")
    {

        List<string> jwtlist = _jwtRepository.ReadJWTToken();
        ViewData["UserEmail"] = jwtlist[0];
        ViewData["UserName"] = jwtlist[1];
        ViewData["RoleName"] = jwtlist[2];

        // Get data from repository (returns a tuple)
        var (users, count, pageSize, pageNumber, sortColumn, sortDirection, searchKey) = await _userRepository.GetUserAsync(PageSize, PageNumber, sortBy, sortOrder, SearchKey);

        // Store metadata in ViewData (converted to correct types)
        ViewData["sortBy"] = sortColumn;
        ViewData["sortOrder"] = sortDirection;
        ViewData["PageSize"] = pageSize;
        ViewData["PageNumber"] = pageNumber;
        ViewData["SearchKey"] = searchKey;
        ViewData["Count"] = count;  // Total user count for pagination
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            // Thread.Sleep(3000);
            return PartialView("~/Views/User/_UserListPartial.cshtml", users); // Return partial view for AJAX
        }
        // Pass only the user list (List<UserListViewModel>) to the View
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
