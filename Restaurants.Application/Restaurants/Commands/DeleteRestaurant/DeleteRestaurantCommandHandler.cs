using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
     IRestaurantsRepository restaurantsRepository,
     IRestauratntAuthorizationServices restauratntAuthorizationServices) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Restaurant with id : {RestaurantId}",request.Id);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id)
        ?? throw new NotFoundException(nameof(Restaurant) ,request.Id.ToString());

        if (restauratntAuthorizationServices.IsAuthorize(ResourceOperation.Delete, restaurant))
            throw new ForbiddenException();

        await restaurantsRepository.DeleteAsync(restaurant);
    }

}
