using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IMenuRepository
{
    List<CategoryViewModel> GetCategoryAsync();
    Task<bool> AddCategoryAsync(string categoryName, string categoryDescription);
    Task DeleteCategoryAsync(int categoryId);
    Task<CategoryViewModel> GetCategoryByIdAsync(int categoryId);
    Task<bool> UpdateCategoryAsync(int CategoryId, string CategoryName, string Description);
    List<ItemListViewModel> GetMenuItemsAsync(int CategoryId);
}
