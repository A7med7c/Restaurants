using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestauratntAuthorizationServices(
    ILogger<RestauratntAuthorizationServices> logger,
    IUserContext userContext) : IRestauratntAuthorizationServices
{
    public bool IsAuthorize(ResourceOperation resourceOperation, Restaurant restaurant)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("Authorizing user {UserEmail} : to operation {Operation} fro restaurant {RestaurantName}",
            currentUser!.Email, resourceOperation, restaurant);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && currentUser.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }
        if (resourceOperation == ResourceOperation.Update || resourceOperation == ResourceOperation.Delete
            && currentUser.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner - successful authorization");
            return true;
        }

        return false;
    }
}
