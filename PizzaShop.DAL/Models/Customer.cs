using System;
using System.Collections;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public int Noofperson { get; set; }

    public BitArray Iswaiting { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Waitinglist> Waitinglists { get; } = new List<Waitinglist>();
}
