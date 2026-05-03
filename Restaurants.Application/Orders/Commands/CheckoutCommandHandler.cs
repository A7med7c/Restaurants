using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Application.Payments;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System.Transactions;

namespace Restaurants.Application.Orders.Commands;

public class CheckoutCommandHandler(
    ILogger<CheckoutCommandHandler> logger,
    IUserContext userContext,
    IMediator mediator,
    ICartRepository cartRepository,
    IOrdersRepository ordersRepository,
    IPaymentService paymentService) : IRequestHandler<CheckoutCommand, CheckoutResultDto>
{
    public async Task<CheckoutResultDto> Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        if (currentUser == null)
        {
            throw new InvalidOperationException("User context is not available.");
        }

        logger.LogInformation("{UserEmail} [{UserId}] Checking out cart", currentUser.Email, currentUser.Id);
        Console.WriteLine("Checking out cart...");

        if (paymentService == null)
        {
            throw new InvalidOperationException("Payment service is not available.");
        }

        var cart = await cartRepository.GetByUserIdAsync(currentUser.Id);
        if (cart == null || cart.Items == null || cart.Items.Count == 0)
        {
            throw new NotFoundException(nameof(Cart), currentUser.Id);
        }

        var totalPrice = cart.Items.Sum(i => i.Quantity * i.Dish.Price);
        if (totalPrice <= 0)
        {
            throw new InvalidOperationException("Total price must be greater than zero.");
        }

        var createOrder = new CreateOrderCommand
        {
            Items = cart.Items.Select(i => new OrderItemDto
            {
                Name = i.Dish.Name,
                Price = i.Dish.Price,
                Quantity = i.Quantity,
                OrderId = 0
            }).ToList()
        };

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        Console.WriteLine("Creating order...");

        var orderId = await mediator.Send(createOrder, cancellationToken);

        var order = await ordersRepository.GetByIdAsync(orderId)
            ?? throw new NotFoundException(nameof(Order), orderId.ToString());

        if (order.Items == null)
        {
            throw new InvalidOperationException("Order items are not initialized.");
        }

        order.PaymentMethod = request.PaymentMethod;
        order.PaymentStatus = PaymentStatus.Pending;

        string? paymentUrl = null;
        if (request.PaymentMethod == PaymentMethod.Paymob)
        {
            Console.WriteLine("Calling payment service...");
            paymentUrl = await paymentService.CreatePaymentAsync(order);
            Console.WriteLine("Payment service completed.");
            order.PaymentReference = paymentUrl;
        }

        await ordersRepository.SaveChanges();

        scope.Complete();

        return new CheckoutResultDto { OrderId = orderId, PaymentUrl = paymentUrl };
    }
}
