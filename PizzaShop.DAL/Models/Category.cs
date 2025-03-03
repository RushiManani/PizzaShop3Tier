using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; } = new List<Menuitem>();
}
