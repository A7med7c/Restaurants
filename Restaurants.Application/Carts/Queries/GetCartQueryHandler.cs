using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Carts.Dtos;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Carts.Queries;

public class GetCartQueryHandler(
    ILogger<GetCartQueryHandler> logger,
    IUserContext userContext,
    ICartRepository cartRepository,
    IMapper mapper) : IRequestHandler<GetCartQuery, CartDto>
{
    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("{UserEmail} [{UserId}] Getting current cart", currentUser!.Email, currentUser.Id);

        var cart = await cartRepository.GetByUserIdAsync(currentUser.Id);
        if (cart == null)
        {
            cart = new Cart { UserId = currentUser.Id };
            await cartRepository.AddAsync(cart);
        }

        return mapper.Map<CartDto>(cart);
    }
}
