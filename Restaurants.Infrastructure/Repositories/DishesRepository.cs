using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishesRepository(RestaurantDbContext dbContext) : IDishesRepository
{
    public async Task<IEnumerable<Dish>> GetAllAsync(int? restaurantId, int? menuCategoryId)
    {
        var query = dbContext.Dishes.AsQueryable();

        if (restaurantId.HasValue)
            query = query.Where(r => r.RestaurantId == restaurantId); 
        
        if (menuCategoryId.HasValue)
            query = query.Where(r => r.MenuCategoryId == menuCategoryId);
        
        var dishes = await query.ToListAsync();
        
        return dishes;

    }

    public async Task<Dish?> GetByIdAsync(int id)
    {
        var dish = await dbContext.Dishes.FirstOrDefaultAsync(d => d.Id == id);
        
        return dish;
    }

    public async Task<int> AddAsync(Dish entity)
    {
        dbContext.Dishes.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var dish = await dbContext.Dishes.FindAsync(id);
        if (dish != null)
        {
            dbContext.Dishes.Remove(dish);
            await dbContext.SaveChangesAsync();
        }
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
