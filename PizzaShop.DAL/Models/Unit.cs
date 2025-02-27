using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Unit
{
    public int UnitId { get; set; }

    public string? UnitName { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; } = new List<Menuitem>();

    public virtual ICollection<Modifieritem> Modifieritems { get; } = new List<Modifieritem>();
}
