namespace PizzaShop.DAL.ViewModel;

public class MenuViewModel
{
    public List<CategoryViewModel> allCategory { get; set; }
    public List<ItemListViewModel> allItems { get; set; }
}

public class CategoryViewModel
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string Description { get; set; } = null;
}

public class ItemListViewModel
{
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public decimal Rate { get; set; }
    public int Quantity { get; set; }
    public bool isAvailable { get; set; }
    public string ItemType { get; set; }
}