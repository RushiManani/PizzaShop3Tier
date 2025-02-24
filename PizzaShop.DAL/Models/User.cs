using System;
using System.Collections.Generic;

namespace PizzaShop.DAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public string? MobileNumber { get; set; }

    public string? Address { get; set; }

    public string? Zipcode { get; set; }

    public bool Isadmin { get; set; }

    public bool? Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public int RoleId { get; set; }

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual State State { get; set; } = null!;
}
