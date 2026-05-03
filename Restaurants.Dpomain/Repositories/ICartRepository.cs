using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetByUserIdAsync(string userId);
    Task<int> AddAsync(Cart cart);
    Task SaveChanges();
}
