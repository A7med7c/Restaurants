namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GatAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
}
