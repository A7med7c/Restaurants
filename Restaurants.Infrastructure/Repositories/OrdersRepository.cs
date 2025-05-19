using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class OrdersRepository(RestaurantDbContext dbContext) : IOrdersRepository
{
    public Task<Order> GetByIdAsync(int id)
    {
        var order = dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
        return order;
    }

    public async Task<int> AddAsync(Order entity)
    {
        dbContext.Orders.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
