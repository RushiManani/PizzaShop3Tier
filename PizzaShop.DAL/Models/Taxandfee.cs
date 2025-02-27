using System;
using System.Collections;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Taxandfee
{
    public int TaxId { get; set; }

    public string TaxName { get; set; } = null!;

    public string TaxType { get; set; } = null!;

    public BitArray Isenabled { get; set; } = null!;

    public BitArray DefaultTax { get; set; } = null!;

    public decimal TaxAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Menuitem> Menuitems { get; } = new List<Menuitem>();
}
