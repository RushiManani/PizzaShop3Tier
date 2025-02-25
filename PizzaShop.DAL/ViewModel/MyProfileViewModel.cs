using PizzaShop.DAL.Models;

namespace PizzaShop.DAL.ViewModel;

public class MyProfileViewModel
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? MobileNumber { get; set; }

    public string? Address { get; set; }

    public string? Zipcode { get; set; }

    public string? ProfilePicture { get; set; }

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    public List<Country>? CountryList {get;set;}
    public List<State>? StateList {get;set;}
    public List<City>? CityList {get;set;}

}

