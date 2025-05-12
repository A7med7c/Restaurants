namespace Restaurants.Domain.Entities;

public interface IDishesRepository
{
    Task<IEnumerable<Dish>> GetAllAsync(int? restaurantId, int? menuCategoryId);
    Task<Dish?> GetByIdAsync(int id);
    Task<int> AddAsync(Dish entity);
    Task DeleteAsync(int id);
    Task SaveChanges();
}
