using Microsoft.AspNetCore.Mvc;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.Controllers;

public class UserController : Controller
{

    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult User_ListView(string? sortorder, string searchString, string currentFilter, int page = 1, int pageSize = 5)
    {

        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortorder) ? "name_desc" : "";
        ViewData["RoleSortParm"] = sortorder == "role" ? "role_desc" : "role";
        ViewData["CurrentSort"] = sortorder;
        ViewData["CurrentFilter"] = searchString;

        List<UserViewModel> users = _userRepository.GetUserAsync(page, pageSize);
        int totalItems = users.Count();
        int totalPages = (int)System.Math.Ceiling((double)totalItems / pageSize);
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.PageSize = pageSize;

        if (!String.IsNullOrEmpty(searchString))
        {
            users = users.Where(s => s.UserName.Contains(searchString)).ToList();
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
}
