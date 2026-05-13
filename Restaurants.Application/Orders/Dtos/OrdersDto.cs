using Restaurants.Domain.Constants;

namespace Restaurants.Application.Orders.Dtos;

public class OrdersDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    public string CustomerId { get; set; } = default!;
}
