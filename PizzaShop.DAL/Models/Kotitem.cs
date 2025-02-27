using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Kotitem
{
    public int TokenId { get; set; }

    public int ItemId { get; set; }

    public int KotId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Menuitem Item { get; set; } = null!;

    public virtual Kot Kot { get; set; } = null!;
}
