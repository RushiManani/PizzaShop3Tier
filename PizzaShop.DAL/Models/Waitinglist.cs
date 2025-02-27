using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Waitinglist
{
    public int WaitinglistId { get; set; }

    public int TableId { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
}
