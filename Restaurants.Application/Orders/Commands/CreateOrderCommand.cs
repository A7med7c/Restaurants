using MediatR;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Orders.Commands;

public class CreateOrderCommand : IRequest<int>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}
