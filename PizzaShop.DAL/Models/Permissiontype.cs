using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Permissiontype
{
    public int PermissiontypeId { get; set; }

    public int PermissionId { get; set; }

    public int RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? CanView { get; set; }

    public bool? CanAddEdit { get; set; }

    public bool? CanDelete { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
