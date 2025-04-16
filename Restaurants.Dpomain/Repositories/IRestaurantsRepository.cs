namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GatAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> CreateAsync(Restaurant entity); 
    Task DeleteAsync(Restaurant entity);
    Task SaveChanges();
}
