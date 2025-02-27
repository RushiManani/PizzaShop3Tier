using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int TableId { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Kot> Kots { get; } = new List<Kot>();

    public virtual ICollection<Mappingorderitemwithmodifier> Mappingorderitemwithmodifiers { get; } = new List<Mappingorderitemwithmodifier>();

    public virtual ICollection<Orderitem> Orderitems { get; } = new List<Orderitem>();

    public virtual ICollection<Payment> Payments { get; } = new List<Payment>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual Table Table { get; set; } = null!;
}
