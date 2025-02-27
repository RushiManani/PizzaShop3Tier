using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Orderitem
{
    public int OrderitemsId { get; set; }

    public int OrderId { get; set; }

    public int ItemId { get; set; }

    public int ModifieritemId { get; set; }

    public int PaymentId { get; set; }

    public int Quantity { get; set; }

    public string? Orderwisecomments { get; set; }

    public string? Itemwisecomments { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();

    public virtual Menuitem Item { get; set; } = null!;

    public virtual Modifieritem Modifieritem { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Payment Payment { get; set; } = null!;
}
