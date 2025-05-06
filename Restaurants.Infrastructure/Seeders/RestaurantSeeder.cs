using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
           
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        return new List<IdentityRole>
        {
            new(UserRoles.User)
            {
                NormalizedName = UserRoles.User.ToUpper()
            },
            new(UserRoles.Admin)
            {
                NormalizedName = UserRoles.Admin.ToUpper()
            },
            new(UserRoles.Owner)
            {
                NormalizedName = UserRoles.Owner.ToUpper()
            },
            new(UserRoles.Driver)
            {
                NormalizedName = UserRoles.Driver.ToUpper()
            },
        };
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        return new List<Restaurant>
        {
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC (short for Kentucky Fried Chicken)...",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Address = new Address
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                },
                MenuCategories = new List<MenuCategory>
                {
                    new()
                    {
                        Name = "Fried Chicken",
                        Dishes = new List<Dish>
                        {
                            new()
                            {
                                Name = "Nashville Hot Chicken",
                                Description = "Nashville Hot Chicken (10 pcs.)",
                                Price = 10.30M
                            },
                            new()
                            {
                                Name = "Chicken Nuggets",
                                Description = "Chicken Nuggets (5 pcs.)",
                                Price = 5.30M
                            }
                        }
                    }
                }
            },
            new()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description = "McDonald's Corporation...",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                },
                MenuCategories = new List<MenuCategory>
                {
                    new()
                    {
                        Name = "Burgers",
                        Dishes = new List<Dish>
                        {
                            new()
                            {
                                Name = "Big Mac",
                                Description = "Classic Big Mac Burger",
                                Price = 8.50M
                            }
                        }
                    }
                }
            }
        };
    }
}
