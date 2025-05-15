using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Dishes.Commands.UpdateDish;

public class UpdateForRestaurantCommandHandler(ILogger<UpdateForRestaurantCommandHandler> logger,
   IMapper mapper,
  // IRestauratntAuthorizationServices restauratntAuthorizationServices,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository)
    : IRequestHandler<UpdateForRestaurantCommand>
{
    public async Task Handle(UpdateForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update dish with id : {DishId} with {@UpdatedDish}", request.Id, request);
       
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        //if (!restauratntAuthorizationServices.IsAuthorize(ResourceOperation.Create, restaurant))
        //    throw new ForbiddenException();

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.Id)
                    ?? throw new NotFoundException(nameof(Dish), request.Id.ToString());
        
        mapper.Map(request, dish);

        await dishesRepository.SaveChanges();
    }
}
