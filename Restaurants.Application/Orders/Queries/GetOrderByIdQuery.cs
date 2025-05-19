using MediatR;
using Restaurants.Application.Orders.Dtos;

namespace Restaurants.Application.Orders.Queries;

public class GetOrderByIdQuery(int id) : IRequest<OrdersDto> 
{
    public int Id { get; } = id;
}
