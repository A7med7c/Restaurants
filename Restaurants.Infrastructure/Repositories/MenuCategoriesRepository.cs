using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class MenuCategoriesRepository(RestaurantDbContext dbContext) : IMenuCategoriesRepository
{
    public async Task<int> AddAsync(MenuCategory entity)
    {
         dbContext.MenuCategories.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await dbContext.MenuCategories.FindAsync(id);
        if (category != null)
        {
            dbContext.MenuCategories.Remove(category);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<MenuCategory> GetByIdAsync(int id)
    {
        return await dbContext.MenuCategories.FindAsync(id);
    }
}
