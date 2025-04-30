//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Restaurants.Domain.Constants;
//using Restaurants.Domain.Entities;
//using Restaurants.Infrastructure.Persistence;

//namespace Restaurants.Infrastructure.Seeders;

//internal class RestaurantSeeder(RestaurantDbContext dbContext) : IRestaurantSeeder
//{
//    public async Task Seed()
//    {
//        if (await dbContext.Database.CanConnectAsync())
//        {
//            var owner = await dbContext.Users.FirstOrDefaultAsync(o => o.Email == "Owner@test.com");
//            if (!dbContext.Restaurants.Any() && owner != null)
//            {
//                var restaurants = GetRestaurants(owner!.Id);
//                dbContext.Restaurants.AddRange(restaurants);
//                await dbContext.SaveChangesAsync();
//            }

            
//            if (!dbContext.Roles.Any())
//            {
//                var roles = GetRoles();
//                dbContext.Roles.AddRange(roles);
//                await dbContext.SaveChangesAsync();
//            }
//        }
//    }

//    private IEnumerable<IdentityRole> GetRoles()
//    {
//        List<IdentityRole> roles =
//            [
//            new (UserRoles.User),
//            new (UserRoles.Admin),
//            new (UserRoles.Owner),
//            new (UserRoles.Driver),
//            ];
//        return roles;
//    }
//    private IEnumerable<Restaurant> GetRestaurants(string ownerId)
//    {
//        List<Restaurant> restaurants = [
//            new()
//        {
//            Name = "KFC",
//            Category = "Fast Food",
//            Description =
//                "KFC (short for Kentucky Fried Chicken)...",
//            ContactEmail = "contact@kfc.com",
//            HasDelivery = true,
//            OwnerId = ownerId,
//            Dishes =
//            [
//                new ()
//                {
//                    Name = "Nashville Hot Chicken",
//                    Description = "Nashville Hot Chicken (10 pcs.)",
//                    Price = 10.30M,
//                },
//                new ()
//                {
//                    Name = "Chicken Nuggets",
//                    Description = "Chicken Nuggets (5 pcs.)",
//                    Price = 5.30M,
//                },
//            ],
//            Address = new ()
//            {
//                City = "London",
//                Street = "Cork St 5",
//                PostalCode = "WC2N 5DU"
//            },
//        },
//        new ()
//        {
//            Name = "McDonald",
//            Category = "Fast Food",
//            Description =
//                "McDonald's Corporation...",
//            ContactEmail = "contact@mcdonald.com",
//            HasDelivery = true,
//            OwnerId = ownerId,
//            Address = new Address()
//            {
//                City = "London",
//                Street = "Boots 193",
//                PostalCode = "W1F 8SR"
//            }
//        }
//        ];

//        return restaurants;
//    }
//}
