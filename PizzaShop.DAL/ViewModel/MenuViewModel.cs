using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PizzaShop.DAL.Models;

namespace PizzaShop.DAL.ViewModel;

public class MenuViewModel
{
    public List<CategoryViewModel>? allCategory { get; set; }
    public List<ItemListViewModel>? allItems { get; set; }
    public List<Category>? categoryDropDown { get; set; }
    public List<Unit>? unitDropDown { get; set; }
    public List<Itemtype>? itemtypeDropDown { get; set; }
    public EditItemListViewModel? EditViewModel { get; set; }
    // public List<AddItemListViewModel>? addItemList { get; set; }
    // public EditItemListViewModel editItemListViewModel{get;set;}
}

public class CategoryViewModel
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string Description { get; set; } = null;
}

public class ItemListViewModel
{
    public int CategoryId { get; set; }
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public decimal Rate { get; set; }
    public int Quantity { get; set; }
    public bool isAvailable { get; set; }
    public string ItemType { get; set; }
    public string ItemPhoto { get; set; }
}

public class AddItemListViewModel
{
    [Required]
    public int CategoryId { get; set; }
    public int ItemId { get; set; }
    [Required]
    public int ItemtypeId { get; set; }
    public string ItemTypeName { get; set; }
    [Required]
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public string CategoryName { get; set; }
    [Required]
    public string ItemName { get; set; }
    public string Description { get; set; }
    [Required]
    public decimal Rate { get; set; }
    [Required]
    public int Quantity { get; set; }
    public IFormFile ItemPhoto { get; set; }
    public int TaxId { get; set; }
    public bool ISDefaultTax { get; set; }
    public bool IsAvailable { get; set; }
    public decimal TaxPercentage { get; set; }
    public string ShortCode { get; set; }
    public List<Category>? categoryDropDown { get; set; }
    public List<Unit>? unitDropDown { get; set; }
    public List<Itemtype>? itemtypeDropDown { get; set; }
}

public class EditItemListViewModel
{
    public int CategoryId { get; set; }
    public int ItemId { get; set; }

    public int ItemtypeId { get; set; }
    public string ItemTypeName { get; set; }

    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public string CategoryName { get; set; }

    public string ItemName { get; set; }
    public string Description { get; set; }
    public decimal Rate { get; set; }

    public int Quantity { get; set; }
    public IFormFile ItemPhoto { get; set; }
    public int TaxId { get; set; }
    public bool ISDefaultTax { get; set; }
    public bool IsAvailable { get; set; }
    public decimal TaxPercentage { get; set; }
    public string ShortCode { get; set; }
    public List<Category>? categoryDropDown { get; set; }
    public List<Unit>? unitDropDown { get; set; }
    public List<Itemtype>? itemtypeDropDown { get; set; }
}