namespace PizzaShop.DAL.ViewModel;

public class UserViewModel
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public bool? Isactive {get;set;}
    public string? MobileNumber { get; set; }
    public string? RoleName { get; set; }
    public Role? Role { get; set; }
    public int pageSize { get; set; }
    public int pageNumber { get; set; }
}

public class Role
{
    public int RoleId{get;set;}
    public string? RoleName{get;set;}
}