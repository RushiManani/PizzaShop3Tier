using PizzaShop.DAL.Models;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IMenuRepository
{

    #region Category
    
    List<CategoryViewModel> GetCategoryAsync();
    Task<bool> AddCategoryAsync(string categoryName, string categoryDescription);
    Task DeleteCategoryAsync(int categoryId);
    Task<CategoryViewModel> GetCategoryByIdAsync(int categoryId);
    Task<bool> UpdateCategoryAsync(int CategoryId, string CategoryName, string Description);
    
    #endregion

    #region DropDowns
    
    List<Category> CategoryDropdDown();
    List<Unit> UnitDropdDown();
    List<Itemtype> ItemTypeDropdDown();
    
    #endregion

    #region MenuItems

    List<ItemListViewModel> GetMenuItemsAsync(int CategoryId);
    List<ItemListViewModel> SearchMenuItemsAsync(int id,string searchText);
    Task<bool> AddMenuItemsAsync(AddItemListViewModel model);
    Task DeleteMenuItemAsync(int itemId);
    Task<bool> UpdateMenuItemAsync(List<AddItemListViewModel> list);
    Task<EditItemListViewModel> GetMenuItemsByIdAsync(int id);

    #endregion
}
