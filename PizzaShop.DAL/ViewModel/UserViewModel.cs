using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PizzaShop.DAL.Models;

namespace PizzaShop.DAL.ViewModel;

public class UserViewModel
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public bool? Isactive {get;set;}
    public string? MobileNumber { get; set; }
    public string? RoleName { get; set; }
    public string? ProfilePicture{get;set;}
}

// public class Role
// {
//     public int RoleId{get;set;}
//     public string? RoleName{get;set;}
// }

public class NewUserModel
{
    public int? UserId{get;set;}
    [Required]
    public string? Password{get;set;}
    public string? FirstName{get;set;}
    public string? LastName{get;set;} = null;
    [Required]
    public string? UserName{get;set;}
    [Required]
    public int RoleId{get;set;}
    [Required]
    public string? Email{get;set;}
    public IFormFile? ProfilePicture{get;set;}=null;
    public int CountryId{get;set;}
    public int StateId{get;set;}
    public int CityId{get;set;}
    public string? Zipcode{get;set;}=null;
    public string? Phone{get;set;}=null;
    public string? Address{get;set;}=null;
    public bool? Isactive{get;set;}
    public List<Country>? CountryList { get; set; }=null;
    public List<State>? StateList { get; set; }=null;
    public List<City>? CityList { get; set; }=null;
    public List<Role>? RoleList {get;set;}=null;
}

public class EditUserModel
{
    public int? UserId{get;set;}
    public string? FirstName{get;set;}
    public string? LastName{get;set;} = null;
    [Required]
    public string? UserName{get;set;}
    [Required]
    public int RoleId{get;set;}
    [Required]
    public string? Email{get;set;}
    public IFormFile? ProfilePicture{get;set;}=null;
    public int CountryId{get;set;}
    public int StateId{get;set;}
    public int CityId{get;set;}
    public string? Zipcode{get;set;}=null;
    public string? Phone{get;set;}=null;
    public string? Address{get;set;}=null;
    public bool? Isactive{get;set;}
    public List<Country>? CountryList { get; set; }=null;
    public List<State>? StateList { get; set; }=null;
    public List<City>? CityList { get; set; }=null;
    public List<Role>? RoleList {get;set;}=null;
}