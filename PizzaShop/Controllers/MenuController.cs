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

    public IActionResult Index()
    {
        MenuViewModel mv = new MenuViewModel();
        mv.allCategory = _menuRepository.GetCategoryAsync();
        mv.allItems = _menuRepository.GetMenuItemsAsync(mv.allCategory[0].CategoryId);
        mv.categoryDropDown = _menuRepository.CategoryDropdDown();
        mv.unitDropDown = _menuRepository.UnitDropdDown();
        mv.itemtypeDropDown = _menuRepository.ItemTypeDropdDown();
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

    [HttpGet]
    public IActionResult MenuItemList(int id)
    {
        Console.WriteLine("Hello Get the id");
        MenuViewModel mv = new MenuViewModel();
        mv.allItems = _menuRepository.GetMenuItemsAsync(id);
        mv.categoryDropDown = _menuRepository.CategoryDropdDown();
        mv.unitDropDown = _menuRepository.UnitDropdDown();
        mv.itemtypeDropDown = _menuRepository.ItemTypeDropdDown();
        return PartialView("~/Views/Menu/_MenuItemsPartial.cshtml", mv);
    }
 
    public IActionResult MenuItemSearch(int id, string searchText)
    {
        MenuViewModel mv = new MenuViewModel();
        mv.allItems = _menuRepository.SearchMenuItemsAsync(id, searchText) ?? new List<ItemListViewModel>();
        return PartialView("~/Views/Menu/_MenuItemsPartial.cshtml", mv);
    }

    [HttpGet]
    public async Task<IActionResult> GetMenuItemByID([FromQuery] int id)
    {
        // MenuViewModel mv = new MenuViewModel();
        // // mv.allItems = _menuRepository.GetMenuItemsAsync(id);
        // mv.categoryDropDown = _menuRepository.CategoryDropdDown();
        // mv.unitDropDown = _menuRepository.UnitDropdDown();
        // mv.itemtypeDropDown = _menuRepository.ItemTypeDropdDown();
        // mv.addItemList = _menuRepository.GetMenuItemsByIdAsync(id);

        MenuViewModel mv = new MenuViewModel();
        mv.EditViewModel = await _menuRepository.GetMenuItemsByIdAsync(id);
        mv.categoryDropDown = _menuRepository.CategoryDropdDown();
        mv.unitDropDown = _menuRepository.UnitDropdDown();
        mv.itemtypeDropDown = _menuRepository.ItemTypeDropdDown();

        if(mv.EditViewModel==null)
        {
            return NotFound();
        }
        return PartialView("~/Views/Menu/_EditModalPartial.cshtml", mv);
    }

    [HttpPost]
    public async Task<IActionResult> AddMenuItem(AddItemListViewModel model)
    {
        bool isAdded = await _menuRepository.AddMenuItemsAsync(model);
        if(isAdded)
        {
            TempData["ToastrMessage"] = "Item Added Successfully";
            TempData["ToastrType"] = "success";
            return Json(new { redirectUrl = Url.Action("Index", "Menu") });
        }
        return Json(new { redirectUrl = Url.Action("Index", "Menu") });
    }

    [HttpPost]
    public async Task<IActionResult> EditMenuItem(int CategoryId, string ItemName, int ItemTypeId, decimal Rate, int Quantity, int UnitId, int TaxPercentage, string ShortCode, string Description, IFormFile ItemPhoto, bool IsAvailable, bool IsDefaultTax)
    {
        if (ModelState.IsValid)
        {
            List<AddItemListViewModel> list = new List<AddItemListViewModel>();
            AddItemListViewModel il = new AddItemListViewModel();
            il.CategoryId = CategoryId;
            il.ItemName = ItemName;
            il.Rate = Rate;
            il.Quantity = Quantity;
            il.UnitId = UnitId;
            il.TaxPercentage = TaxPercentage;
            il.ShortCode = ShortCode;
            il.Description = Description;
            il.ItemPhoto = ItemPhoto;
            il.IsAvailable = IsAvailable;
            il.ISDefaultTax = IsDefaultTax;
            il.ItemtypeId = ItemTypeId;
            list.Add(il);
            bool isupdated = await _menuRepository.UpdateMenuItemAsync(list);
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

    public IActionResult DeleteMenuItem(int id)
    {
        Console.WriteLine(id);
        if (id != 0)
        {
            _menuRepository.DeleteMenuItemAsync(id);
            TempData["ToastrMessage"] = "Menu Item Deleted Successfully";
            TempData["ToastrType"] = "success";
            return Json(new { redirectUrl = Url.Action("Index", "Menu") });
        }
        return Json(new { redirectUrl = Url.Action("Index", "Menu") });
    }

    #endregion
}
