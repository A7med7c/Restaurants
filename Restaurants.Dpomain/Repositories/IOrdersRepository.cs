using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IOrdersRepository
{
    Task<Order> GetByIdAsync(int id);
    Task<int> AddAsync(Order entity);
    Task SaveChanges();
}
