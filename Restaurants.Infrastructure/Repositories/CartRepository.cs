using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class CartRepository(RestaurantDbContext dbContext) : ICartRepository
{
    public Task<Cart?> GetByUserIdAsync(string userId)
    {
        return dbContext.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Dish)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<int> AddAsync(Cart cart)
    {
        dbContext.Carts.Add(cart);
        await dbContext.SaveChangesAsync();
        return cart.Id;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
