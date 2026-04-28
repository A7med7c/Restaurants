using MediatR;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Orders.Commands;

public class UpdateOrderStatusCommand : IRequest
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
}
