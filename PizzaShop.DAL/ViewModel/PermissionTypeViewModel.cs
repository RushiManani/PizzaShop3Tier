namespace PizzaShop.DAL.ViewModel;

public class PermissionTypeViewModel
{
    public int permissionTypeId { get; set; }
    public int permissionId { get; set; }
    public string? permissionName { get; set; }
    public int roleId { get; set; }
    public string? roleName { get; set; }
    public bool canView { get; set; }
    public bool canAddEdit { get; set; }
    public bool canDelete { get; set; }
}

