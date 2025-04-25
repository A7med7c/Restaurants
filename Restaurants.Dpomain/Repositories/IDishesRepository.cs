namespace Restaurants.Domain.Repositories;

public interface IDishesRepository
{
    Task<IEnumerable<Dish>> GetAllAsync();
    Task<int> CreateAsync(Dish entity);
}
