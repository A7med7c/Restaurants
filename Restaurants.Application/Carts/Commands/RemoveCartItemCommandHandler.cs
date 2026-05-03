using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Carts.Commands;

public class RemoveCartItemCommandHandler(
    ILogger<RemoveCartItemCommandHandler> logger,
    IUserContext userContext,
    ICartRepository cartRepository) : IRequestHandler<RemoveCartItemCommand>
{
    public async Task Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("{UserEmail} [{UserId}] Removing cart item {CartItemId}",
            currentUser!.Email, currentUser.Id, request.Id);

        var cart = await cartRepository.GetByUserIdAsync(currentUser.Id)
            ?? throw new NotFoundException(nameof(Cart), currentUser.Id);

        var item = cart.Items.FirstOrDefault(i => i.Id == request.Id)
            ?? throw new NotFoundException(nameof(CartItem), request.Id.ToString());

        cart.Items.Remove(item);
        await cartRepository.SaveChanges();
    }
}
