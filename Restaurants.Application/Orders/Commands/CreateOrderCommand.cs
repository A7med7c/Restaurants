using MediatR;
using Restaurants.Application.Orders.Dtos;

namespace Restaurants.Application.Orders.Commands;

public class CreateOrderCommand : IRequest<int>
{
    public List<OrderItemDto> Items { get; set; } = new();
}
