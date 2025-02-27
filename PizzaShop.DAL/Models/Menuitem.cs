using System;
using System.Collections;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Menuitem
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public int CategoryId { get; set; }

    public int ItemtypeId { get; set; }

    public BitArray Isavailable { get; set; } = null!;

    public int UnitId { get; set; }

    public decimal Rate { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public string? ItemPhoto { get; set; }

    public BitArray Isdeleted { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public int TaxId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Itemtype Itemtype { get; set; } = null!;

    public virtual ICollection<Kotitem> Kotitems { get; } = new List<Kotitem>();

    public virtual ICollection<Orderitem> Orderitems { get; } = new List<Orderitem>();

    public virtual Taxandfee Tax { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
