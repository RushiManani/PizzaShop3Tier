using System.ComponentModel.DataAnnotations;

namespace PizzaShop.DAL.ViewModel;

public class AuthViewModel
{

    public int UserId{get;set;}
    [Required]
    public int Email{get;set;}
    
    [Required]
    public string? Password{get;set;}
}
