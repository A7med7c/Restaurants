using Restaurants.Domain.Constants;

namespace Restaurants.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public string CustomerId { get; set; } = default!;
    public User Customer { get; set; } = default!;
}
