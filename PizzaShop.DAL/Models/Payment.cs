using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string PaymentMode { get; set; } = null!;

    public int OrderId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; } = new List<Orderitem>();
}
