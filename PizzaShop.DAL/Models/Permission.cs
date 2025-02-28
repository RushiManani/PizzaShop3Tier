using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Permissiontype> Permissiontypes { get; } = new List<Permissiontype>();
}
