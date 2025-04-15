using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, 
    ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all Restaurants");
        var restaurants = await restaurantsRepository.GatAllAsync();

        //Manual Mapping
        // var restaurantsDtos = restaurants.Select(RestaurantDto.FromEntity);

        // auto Mapper 
        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantsDtos!;
    }

    public async Task<RestaurantDto?> GetRestaurant(int id)
    {
        logger.LogInformation($"Get Restaurant {id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(id);

        //Manual Mapping
        //var restaurantDto = RestaurantDto.FromEntity(restaurant);

        //AutoMapper
        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }
}
