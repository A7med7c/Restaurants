using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Orders.Dtos;

public class OrdersDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; private set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public string CustomerId { get; set; } = default!;
}
