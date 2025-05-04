using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class MenuCategoriesRepository(RestaurantDbContext dbContext) : IMenuCategoriesRepository
{
    public async Task<int> CreateAsync(MenuCategory entity)
    {
         dbContext.MenuCategories.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }
}
