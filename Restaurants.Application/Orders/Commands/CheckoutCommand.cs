using MediatR;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Orders.Commands;

public class CheckoutCommand : IRequest<CheckoutResultDto>
{
    public PaymentMethod PaymentMethod { get; set; }
}
