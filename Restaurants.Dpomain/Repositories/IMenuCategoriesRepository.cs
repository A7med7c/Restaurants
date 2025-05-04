using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IMenuCategoriesRepository
{
    Task<int> CreateAsync(MenuCategory entity); 
}
