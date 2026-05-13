using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Commands.UploadLogo;

internal class UploadRestaurantLogoCommandHandler(ILogger<UploadRestaurantLogoCommand> logger,
    IRestaurantsRepository restaurantsRepository, IRestauratntAuthorizationServices restauratntAuthorizationServices,
    IBlobStorageService blobStorageService)
    : IRequestHandler<UploadRestaurantLogoCommand>
{
    public async Task Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Uploading Restaurant logo for id : {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!restauratntAuthorizationServices.IsAuthorize(ResourceOperation.Update, restaurant))
            throw new ForbiddenException();

        var logoUrl = await blobStorageService.UploadToBlobAsync(request.FileContent, request.FileName, cancellationToken);

        restaurant.LogoUrl = logoUrl;
        await restaurantsRepository.SaveChanges();
    }
}
