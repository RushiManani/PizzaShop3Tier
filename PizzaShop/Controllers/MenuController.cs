using Microsoft.AspNetCore.Mvc;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.Controllers;

public class MenuController : Controller
{

    private readonly IMenuRepository _menuRepository;

    public MenuController(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public IActionResult Index()
    {
        List<CategoryViewModel> list = _menuRepository.GetCategoryAsync();
        return View(list);
    }

    public async Task<IActionResult> AddCategory(string categoryName, string categoryDescription)
    {
        Console.WriteLine(categoryName + " -- " + categoryDescription);
        bool isAdded = await _menuRepository.AddCategoryAsync(categoryName, categoryDescription);
        if (isAdded)
        {
            return Json(new { redirectUrl = Url.Action("Index", "Menu") });
        }
        return Json(new { redirectUrl = Url.Action("Index", "Menu") });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCategory(int CategoryId, string CategoryName, string Description="")
    {
        if (ModelState.IsValid)
        {
            bool isupdated = await _menuRepository.UpdateCategoryAsync(CategoryId, CategoryName, Description);
            if (isupdated)
            {
                TempData["ToastrMessage"] = "Category Updated Successfully";
                TempData["ToastrType"] = "success";
                return Json(new { redirectUrl = Url.Action("Index", "Menu") });
            }
            TempData["ToastrMessage"] = "Account Already Exists with this Username";
            TempData["ToastrType"] = "error";
            return Json(new { redirectUrl = Url.Action("Index", "Menu") });
        }

        return Json(new { redirectUrl = Url.Action("Index", "Menu") });
    }

    public IActionResult DeleteCategory(int id)
    {
        Console.WriteLine(id);
        if (id != 0)
        {
            _menuRepository.DeleteCategoryAsync(id);
            TempData["ToastrMessage"] = "Category Deleted Successfully";
            TempData["ToastrType"] = "success";
            return Json(new { redirectUrl = Url.Action("Index", "Menu") });
        }
        return Json(new { redirectUrl = Url.Action("Index", "Menu") });
    }
}
