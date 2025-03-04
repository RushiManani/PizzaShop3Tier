using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Itemtype
{
    public int ItemtypeId { get; set; }

    public string ItemtypeName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; } = new List<Menuitem>();
}
