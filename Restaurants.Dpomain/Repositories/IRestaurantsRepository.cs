namespace Restaurants.Domain.Entities;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> CreateAsync(Restaurant entity); 
    Task DeleteAsync(Restaurant entity);
    Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase);
    Task SaveChanges();
}
