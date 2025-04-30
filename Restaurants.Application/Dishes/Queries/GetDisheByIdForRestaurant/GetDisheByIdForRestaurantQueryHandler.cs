//using AutoMapper;
//using MediatR;
//using Microsoft.Extensions.Logging;
//using Restaurants.Application.Dishes.Dtos;
//using Restaurants.Domain.Exceptions;
//using Restaurants.Domain.Entities;

//namespace Restaurants.Application.Dishes.Queries.GetDisheByIdForRestaurant;

//public class GetDisheByIdForRestaurantQueryHandler(ILogger<GetDisheByIdForRestaurantQueryHandler> logger,
//    IRestaurantsRepository restaurantsRepository, IMapper mapper) 
//    : IRequestHandler<GetDisheByIdForRestaurantQuery,DishesDto>
//{
//    async Task<DishesDto> IRequestHandler<GetDisheByIdForRestaurantQuery, DishesDto>.Handle(GetDisheByIdForRestaurantQuery request, CancellationToken cancellationToken)
//    {
//        logger.LogInformation("Get dish with id {Id} for restaurant with id {RestaurantId}", request.RestaurantId, request.DishId);
       
//        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
//        if (restaurant == null)
//            throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());

//        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
//        if (dish == null)
//            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        
//        var result = mapper.Map<DishesDto>(dish);
//        return result;
//    }
//}
