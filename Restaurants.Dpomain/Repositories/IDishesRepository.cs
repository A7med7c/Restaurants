namespace Restaurants.Domain.Entities;

public interface IDishesRepository
{
    Task<int> CreateAsync(Dish entity);
    Task DeleteAsync(Dish entity);
    Task SaveChanges();
}
