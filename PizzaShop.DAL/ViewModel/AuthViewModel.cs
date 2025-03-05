using System.ComponentModel.DataAnnotations;

namespace PizzaShop.DAL.ViewModel;

public class AuthViewModel
{

    [Required]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
    public string? Email{get;set;}
    [Required]
    public string? Password{get;set;}
    public bool RememberMe{get;set;} = false;
}

public class ForgotPassword
{
    [Required]
    public string? Email { get; set; }
}

public class ResetPassword
{

    [Required]
    [MinLength(8, ErrorMessage = "Password must be minimum 8 character")]
    public string NewPassword { get; set; } = null;

    [Required(ErrorMessage = "Confirm Password do not Match with Password")]
    [MinLength(8, ErrorMessage = "Password must be minimum 8 character")]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; } = null;
}

public class ChangePassword
{

    [Required]
    [MinLength(8, ErrorMessage = "Password must be minimum 8 character")]
    public string CurrentPassword { get; set; } = null;

    [Required]
    [MinLength(8, ErrorMessage = "Password must be minimum 8 character")]
    public string NewPassword { get; set; } = null;

    [Required(ErrorMessage = "Confirm Password do not Match with Password")]
    [MinLength(8, ErrorMessage = "Password must be minimum 8 character")]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; } = null;
}