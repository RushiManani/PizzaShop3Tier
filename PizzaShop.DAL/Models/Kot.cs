using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Kot
{
    public int KotId { get; set; }

    public int OrderId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Kotitem> Kotitems { get; } = new List<Kotitem>();

    public virtual Order Order { get; set; } = null!;
}
