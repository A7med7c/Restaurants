using MediatR;
using Restaurants.Application.Carts.Dtos;

namespace Restaurants.Application.Carts.Queries;

public class GetCartQuery : IRequest<CartDto>
{
}
