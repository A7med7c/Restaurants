using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;
internal class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GatAllAsync()
    {
        var restaurants = await dbContext.Restaurants
            .Include(r => r.Dishes) // Include the related dishes
            .ToListAsync();
        return restaurants;
    }


    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);
            
        return restaurant;
    }
}
