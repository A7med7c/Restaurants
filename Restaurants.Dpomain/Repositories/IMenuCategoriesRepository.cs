using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IMenuCategoriesRepository
{
    Task<MenuCategory> GetByIdAsync(int id);
    Task<int> AddAsync(MenuCategory entity);
    Task DeleteAsync(int id);
}
