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

    #region Category Crud

    public ActionResult Index(int id)
    {
        MenuViewModel mv = new MenuViewModel();
        mv.allCategory = GetCategory();
        id = id == 0 ? mv.allCategory[0].CategoryId : id;
        mv.allItems = MenuItemList(id);
        return View(mv);
    }

    public List<CategoryViewModel> GetCategory()
    {
        List<CategoryViewModel> categoryList = new List<CategoryViewModel>();
        categoryList = _menuRepository.GetCategoryAsync();
        return categoryList;
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
    public async Task<IActionResult> UpdateCategory(int CategoryId, string CategoryName, string Description = "")
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

    #endregion 

    #region  MenuItem Crud

    public List<ItemListViewModel> MenuItemList(int id)
    {
        List<ItemListViewModel> list = _menuRepository.GetMenuItemsAsync(id);
        return list;
    }

    #endregion
}
