using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Commands;

public class CreateOrderCommandHandler(
    IUserContext userContext,
    ILogger<CreateOrderCommand> logger,
    IMapper mapper,
    IOrdersRepository ordersRepository
    ) : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("{UserEmail} [{UserId}] Creating a new Order{@Order}",
            currentUser!.Email, currentUser.Id, request);

        var order = new Order { CustomerId = currentUser.Id };
        foreach (var itemDto in request.Items)
        {
            var item = mapper.Map<OrderItem>(itemDto);
            order.AddItem(item);
        }

        await ordersRepository.AddAsync(order);

        return order.Id;
    }
}
