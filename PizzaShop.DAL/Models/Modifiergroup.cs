using System;
using System.Collections;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Modifiergroup
{
    public int ModifiergroupId { get; set; }

    public string ModifiergroupName { get; set; } = null!;

    public string? Description { get; set; }

    public BitArray Isdeleted { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Mappingmodifiergroupwithitem> Mappingmodifiergroupwithitems { get; } = new List<Mappingmodifiergroupwithitem>();

    public virtual ICollection<Modifieritem> Modifieritems { get; } = new List<Modifieritem>();
}
