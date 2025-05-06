using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces;

public interface IRestauratntAuthorizationServices
{
    bool IsAuthorize(ResourceOperation resourceOperation, Restaurant restaurant);
}