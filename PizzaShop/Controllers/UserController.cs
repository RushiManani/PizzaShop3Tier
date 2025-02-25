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

    public IActionResult User_ListView(int page=1, int pageSize=5)
    {
        List<UserViewModel> users = _userRepository.GetUserAsync(page,pageSize);
        int totalItems = users.Count();
        int totalPages = (int)System.Math.Ceiling((double)totalItems / pageSize);
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.PageSize = pageSize;
        
        return View(users);
    }
}
