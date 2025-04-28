using Microsoft.AspNetCore.Identity;

namespace Restaurants.Domain.Entities;

public class User : IdentityUser
{
    public string? Nationality { get; set; }
    public DateOnly? DateOfBirth { get; set; }
} 
