using System;
using System.Collections;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class Permissiontype
{
    public int PermissiontypeId { get; set; }

    public int PermisssionId { get; set; }

    public int RoleId { get; set; }

    public BitArray CanView { get; set; } = null!;

    public BitArray CanAddEdit { get; set; } = null!;

    public BitArray CanDelete { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Permission Permisssion { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
