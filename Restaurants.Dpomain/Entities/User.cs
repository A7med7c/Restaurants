using Microsoft.AspNetCore.Identity;

namespace Restaurants.Domain.Entities;

public class User : IdentityUser
{
    public string? Nationality { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public Address? Address { get; set; }
    public List<Restaurant> Restaurants { get; set; } = new();
    public List<Order> Orders { get; set; } = new();

}
