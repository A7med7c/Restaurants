using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IOrdersRepository
{
    Task<int> AddAsync(Order entity);
    Task SaveChanges();
}
