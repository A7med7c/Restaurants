using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishesForRestaurantCommandHandler(
    ILogger<DeleteDishesForRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IRestauratntAuthorizationServices restauratntAuthorizationServices)
    : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Delete dish with Id: {Id}", request.Id);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!restauratntAuthorizationServices.IsAuthorize(ResourceOperation.Delete, restaurant))
            throw new ForbiddenException();

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id ==request.Id)
           ?? throw new NotFoundException(nameof(Dish), request.Id.ToString());

          await dishesRepository.DeleteAsync(dish.Id);
    }
}
