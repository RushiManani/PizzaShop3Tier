using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Table
{
    public int TableId { get; set; }

    public string TableName { get; set; } = null!;

    public int Capacity { get; set; }

    public int StatusId { get; set; }

    public int SectionId { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Section Section { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Waitinglist> Waitinglists { get; } = new List<Waitinglist>();
}
