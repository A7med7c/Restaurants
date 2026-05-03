using FluentValidation;

namespace Restaurants.Application.Orders.Commands;

public class CheckoutCommandValidator : AbstractValidator<CheckoutCommand>
{
    public CheckoutCommandValidator()
    {
        RuleFor(c => c.PaymentMethod)
            .IsInEnum();
    }
}
