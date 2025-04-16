using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantbyId;
public class GetRestaurantbyIdQuery(int id) : IRequest<RestaurantDto>
{
    public int Id { get; } = id;
}
