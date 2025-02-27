using System;
using System.Collections;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Section
{
    public int SectionId { get; set; }

    public string SectionName { get; set; } = null!;

    public string? Description { get; set; }

    public BitArray Isdeleted { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Table> Tables { get; } = new List<Table>();
}
