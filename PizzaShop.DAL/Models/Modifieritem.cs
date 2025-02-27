using System;
using System.Collections;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Modifieritem
{
    public int ModifieritemId { get; set; }

    public string ModifieritemName { get; set; } = null!;

    public int ModifiergroupId { get; set; }

    public int UnitId { get; set; }

    public decimal Rate { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public BitArray Isdeleted { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Mappingmodifiergroupwithitem> Mappingmodifiergroupwithitems { get; } = new List<Mappingmodifiergroupwithitem>();

    public virtual ICollection<Mappingorderitemwithmodifier> Mappingorderitemwithmodifiers { get; } = new List<Mappingorderitemwithmodifier>();

    public virtual Modifiergroup Modifiergroup { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; } = new List<Orderitem>();

    public virtual Unit Unit { get; set; } = null!;
}
