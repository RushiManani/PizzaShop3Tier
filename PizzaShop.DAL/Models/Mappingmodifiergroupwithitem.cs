using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Mappingmodifiergroupwithitem
{
    public int MappingmodifiergroupwithitemId { get; set; }

    public int ModifiergroupId { get; set; }

    public int ModifieritemId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Modifiergroup Modifiergroup { get; set; } = null!;

    public virtual Modifieritem Modifieritem { get; set; } = null!;
}
