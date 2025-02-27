using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Mappingorderitemwithmodifier
{
    public int Mappingorderitemwithmodifier1 { get; set; }

    public int OrderitemsId { get; set; }

    public int ModifieritemId { get; set; }

    public virtual Modifieritem Modifieritem { get; set; } = null!;

    public virtual Order Orderitems { get; set; } = null!;
}
