using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? FoodReview { get; set; }

    public int? ServiceReview { get; set; }

    public int? AmbienceReview { get; set; }

    public int OrderId { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
