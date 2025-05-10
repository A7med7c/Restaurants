using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;
internal class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants
            .ToListAsync();
        return restaurants;
    }
    public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        
        var baseQuery = dbContext
             .Restaurants
             .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                    || r.Description.ToLower().Contains(searchPhraseLower)));
        
        
        var totalCount = await baseQuery.CountAsync();
        
        if(sortBy != null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), c => c.Name},
                { nameof(Restaurant.Category), c => c.Category},
            };

            var selectedColumn = columnSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Descending
          ? baseQuery.OrderByDescending(selectedColumn)
          : baseQuery.OrderBy(selectedColumn);
        }

        var restaurants = await baseQuery
                             .Skip(pageSize * (pageNumber - 1))
                             .Take(pageSize)
                             .ToListAsync();


        return (restaurants, totalCount);
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
