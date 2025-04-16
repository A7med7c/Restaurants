using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantbyId;

public class GetRestaurantbyIdQueryHandler(IRestaurantsRepository restaurantsRepository,
    ILogger<GetRestaurantbyIdQueryHandler> logger, IMapper mapper) : IRequestHandler<GetRestaurantbyIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantbyIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Get Restaurant {request.Id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        //Manual Mapping
        //var restaurantDto = RestaurantDto.FromEntity(restaurant);

        //AutoMapper
        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }
}
