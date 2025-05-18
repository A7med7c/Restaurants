using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class OrdersRepository(RestaurantDbContext dbContext) : IOrdersRepository
{
    public async Task<int> AddAsync(Order entity)
    {
        dbContext.Orders.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
