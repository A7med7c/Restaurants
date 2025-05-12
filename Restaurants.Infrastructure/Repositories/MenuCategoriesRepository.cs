using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class MenuCategoriesRepository(RestaurantDbContext dbContext) : IMenuCategoriesRepository
{
    public async Task<IEnumerable<MenuCategory>> GetAllAsync(int? restaurantId)
    {
        var query = dbContext.MenuCategories.AsQueryable();
       
        if (restaurantId.HasValue)
            query = query.Where(c => c.RestaurantId == restaurantId);
       
        return await query.ToListAsync();
    }

    public async Task<MenuCategory?> GetByIdAsync(int id)
    {
        return await dbContext.MenuCategories
            .Include(d => d.Dishes)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
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
}
