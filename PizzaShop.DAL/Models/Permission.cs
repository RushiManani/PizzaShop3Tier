using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<Permissiontype> Permissiontypes { get; } = new List<Permissiontype>();

    public virtual Role Role { get; set; } = null!;
}
