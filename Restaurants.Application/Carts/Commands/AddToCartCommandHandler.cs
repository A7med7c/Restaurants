using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Carts.Commands;

public class AddToCartCommandHandler(
    ILogger<AddToCartCommandHandler> logger,
    IUserContext userContext,
    ICartRepository cartRepository) : IRequestHandler<AddToCartCommand>
{
    public async Task Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("{UserEmail} [{UserId}] Adding item to cart {@CartItem}",
            currentUser!.Email, currentUser.Id, request);

        var cart = await cartRepository.GetByUserIdAsync(currentUser.Id);
        if (cart == null)
        {
            cart = new Cart { UserId = currentUser.Id };
            cart.Items.Add(new CartItem { DishId = request.DishId, Quantity = request.Quantity });
            await cartRepository.AddAsync(cart);
            return;
        }

        var existingItem = cart.Items.FirstOrDefault(i => i.DishId == request.DishId);
        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            cart.Items.Add(new CartItem { DishId = request.DishId, Quantity = request.Quantity });
        }

        await cartRepository.SaveChanges();
    }
}
