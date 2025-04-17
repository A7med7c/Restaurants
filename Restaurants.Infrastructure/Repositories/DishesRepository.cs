using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishesRepository(RestaurantDbContext dbContext) : IDishesRepository
{
    public async Task<IEnumerable<Dish>> GetAllAsync()
    {
        var dishes = await dbContext.Dishes
            .ToListAsync();
      
        return dishes;
    }
}
