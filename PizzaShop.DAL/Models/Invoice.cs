using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public int OrderitemsId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Orderitem Orderitems { get; set; } = null!;
}
