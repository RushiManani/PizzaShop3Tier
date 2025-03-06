using Microsoft.EntityFrameworkCore;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.Data;
using PizzaShop.DAL.Models;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Repository;

public class MenuRepository : IMenuRepository
{
    private readonly PizzaShopDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public MenuRepository(PizzaShopDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
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
            var category = _dbContext.Categories.Where(c => c.CategoryName.ToLower() == categoryName.ToLower()).ToList();
            if (category.Count == 0)
            {
                Category c = new Category();
                c.CategoryName = categoryName;
                c.Description = categoryDescription;
                c.CreatedAt = DateTime.Now;
                c.CreatedBy = "Super Admin";
                _dbContext.Add(c);
                await _dbContext.SaveChangesAsync();
                return true;
            }else{
                category[0].Isdeleted = false;
                _dbContext.Update(category[0]);
                await _dbContext.SaveChangesAsync();
                return true;
            }
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
                         where menuitem.CategoryId == CategoryId && menuitem.IsDeleted == false
                         select new ItemListViewModel
                         {
                             CategoryId = menuitem.CategoryId,
                             ItemId = menuitem.ItemId,
                             ItemName = menuitem.ItemName,
                             ItemType = itemtype.ItemtypeName,
                             Quantity = menuitem.Quantity,
                             Rate = menuitem.Rate,
                             isAvailable = (bool)menuitem.IsAvailable!,
                             ItemPhoto = menuitem.ItemPhoto!
                         }).ToList();

        return menuItems;
    }

    public List<ItemListViewModel> SearchMenuItemsAsync(int id, string searchText = "")
    {
        var menuItem = (from menuitem in _dbContext.Menuitems
                        join itemtype in _dbContext.Itemtypes
                        on menuitem.ItemtypeId equals itemtype.ItemtypeId
                        where menuitem.CategoryId == id && menuitem.ItemName.ToLower().Contains(searchText.ToLower())
                        select new ItemListViewModel
                        {
                            CategoryId = menuitem.CategoryId,
                            ItemId = menuitem.ItemId,
                            ItemName = menuitem.ItemName,
                            ItemType = itemtype.ItemtypeName,
                            Quantity = menuitem.Quantity,
                            Rate = menuitem.Rate,
                            isAvailable = (bool)menuitem.IsAvailable!
                        }).ToList();

        if (menuItem.Count == 0)
        {
            return new List<ItemListViewModel>();
        }

        return menuItem;
    }

    public List<AddItemListViewModel> GetMenuItemsByIdAsync(int id)
    {
        var items = (from menuitem in _dbContext.Menuitems
                     join itemtype in _dbContext.Itemtypes
                     on menuitem.ItemtypeId equals itemtype.ItemtypeId
                     where menuitem.ItemId == id
                     select new AddItemListViewModel
                     {
                         CategoryId = menuitem.CategoryId,
                         Description = menuitem.Description!,
                         ItemName = menuitem.ItemName,
                         IsAvailable = (bool)menuitem.IsAvailable!,
                         Quantity = menuitem.Quantity,
                         Rate = menuitem.Rate,
                         UnitId = menuitem.UnitId,
                         ItemId = menuitem.ItemId,
                         ItemtypeId = menuitem.ItemtypeId
                     }).ToList();
        return items;
    }

    public async Task<bool> AddMenuItemsAsync(List<AddItemListViewModel> list)
    {
        if (list.Count() > 0)
        {
            Menuitem mt = new Menuitem();
            mt.CategoryId = list[0].CategoryId;
            mt.CreatedAt = DateTime.Now;
            mt.CreatedBy = "Super Admin";
            mt.Description = list[0].Description;
            mt.IsAvailable = list[0].IsAvailable;
            mt.ItemName = list[0].ItemName;
            mt.ItemPhoto = await _userRepository.UploadPhotoAsync(list[0].ItemPhoto);
            mt.ItemtypeId = list[0].ItemtypeId;
            mt.Quantity = list[0].Quantity;
            mt.Rate = list[0].Rate;
            mt.UnitId = list[0].UnitId;
            _dbContext.Add(mt);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task DeleteMenuItemAsync(int itemId)
    {
        var item = _dbContext.Menuitems.FirstOrDefault(i => i.ItemId == itemId);
        if (item != null)
        {
            item.IsDeleted = true;
            _dbContext.Update(item);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> UpdateMenuItemAsync(List<AddItemListViewModel> list)
    {
        var menuitem = _dbContext.Menuitems.FirstOrDefault(i => i.ItemId == list[0].ItemId);
        menuitem!.CategoryId = list[0].CategoryId;
        menuitem.Description = list[0].Description;
        menuitem.IsAvailable = list[0].IsAvailable;
        menuitem.ItemName = list[0].ItemName;
        menuitem.ItemPhoto = await _userRepository.UploadPhotoAsync(list[0].ItemPhoto);
        menuitem.ItemtypeId = list[0].ItemtypeId;
        menuitem.Quantity = list[0].Quantity;
        menuitem.Rate = list[0].Rate;
        menuitem.UnitId = list[0].UnitId;
        menuitem.UpdatedAt = DateTime.Now;

        _dbContext.Update(menuitem);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    #endregion

    #region DropDowns

    public List<Category> CategoryDropdDown()
    {
        return _dbContext.Categories.Where(c => c.Isdeleted == false).ToList();
    }

    public List<Unit> UnitDropdDown()
    {
        return _dbContext.Units.ToList();
    }

    public List<Itemtype> ItemTypeDropdDown()
    {
        return _dbContext.Itemtypes.ToList();
    }

    #endregion

}
