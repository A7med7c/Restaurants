namespace Restaurants.Domain.Entities;

public interface IDishesRepository
{
    Task<int> AddAsync(Dish entity);
    Task DeleteAsync(int id);
    Task SaveChanges();
}
