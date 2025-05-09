using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;
internal class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants
            .ToListAsync();
        return restaurants;
    }
    public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var restaurants = await dbContext.Restaurants
            
         .Where(r => searchPhraseLower == null || 
         (r.Name.ToLower().Contains(searchPhraseLower) || r.Description.ToLower().Contains(searchPhraseLower)))
        .ToListAsync();
        return restaurants;
    }


    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(r => r.MenuCategories)
            .ThenInclude(d => d.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);
            
        return restaurant;
    }

    public async Task<int> CreateAsync(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAsync(Restaurant entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public  Task SaveChanges() => dbContext.SaveChangesAsync(); 
}
