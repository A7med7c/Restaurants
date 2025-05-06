using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
public class UpdateRestaurantCommandHandler(
    ILogger<UpdateRestaurantCommand> logger
    , IRestaurantsRepository restaurantsRepository,
    IMapper mapper,
    IRestauratntAuthorizationServices restauratntAuthorizationServices) 
    : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update Restaurant with id : {RestaurantId} with {@Updatedrestaurant}",request.Id,request);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id) 
            ??throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if (restauratntAuthorizationServices.IsAuthorize(ResourceOperation.Update, restaurant))
            throw new ForbiddenException();

        mapper.Map(request, restaurant);

        await restaurantsRepository.SaveChanges();
    }
}
