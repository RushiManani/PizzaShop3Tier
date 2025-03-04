using Microsoft.EntityFrameworkCore;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.Data;
using PizzaShop.DAL.Models;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Repository;

public class MenuRepository : IMenuRepository
{
    private readonly PizzaShopDbContext _dbContext;

    public MenuRepository(PizzaShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Category CRUD

    public List<CategoryViewModel> GetCategoryAsync()
    {
        var category = _dbContext.Categories.Where(c => c.Isdeleted == false).ToList();
        List<CategoryViewModel> list = new List<CategoryViewModel>();
        foreach (var i in category)
        {
            CategoryViewModel model = new CategoryViewModel();
            model.CategoryId = i.CategoryId;
            model.CategoryName = i.CategoryName;
            list.Add(model);
        }
        return list;
    }

    public async Task<bool> AddCategoryAsync(string categoryName, string categoryDescription)
    {
        try
        {
            Category c = new Category();
            c.CategoryName = categoryName;
            c.Description = categoryDescription;
            c.CreatedAt = DateTime.Now;
            c.CreatedBy = "Super Admin";
            _dbContext.Add(c);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var category = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        if (category != null)
        {
            category.Isdeleted = true;
            _dbContext.Update(category);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<CategoryViewModel> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.CategoryId == categoryId);
        var categoryEdit = new CategoryViewModel
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description
        };
        return categoryEdit;
    }

    public async Task<bool> UpdateCategoryAsync(int CategoryId, string CategoryName, string Description)
    {
        var category = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
        category.CategoryName = CategoryName;
        category.Description = Description;
        _dbContext.Update(category);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    #endregion

    #region Menu Item CRUD

    public List<ItemListViewModel> GetMenuItemsAsync(int CategoryId)
    {
        var menuItems = (from menuitem in _dbContext.Menuitems
                         join itemtype in _dbContext.Itemtypes
                         on menuitem.ItemtypeId equals itemtype.ItemtypeId
                         where menuitem.CategoryId == CategoryId
                         select new ItemListViewModel
                         {
                            ItemId = menuitem.ItemId,
                            ItemName = menuitem.ItemName,
                            ItemType = itemtype.ItemtypeName,
                            Quantity = menuitem.Quantity,
                            Rate = menuitem.Rate,
                            isAvailable = (bool)menuitem.IsAvailable!
                         }).ToList();

        return menuItems;
    }

    #endregion

}
