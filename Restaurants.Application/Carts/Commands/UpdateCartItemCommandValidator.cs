using FluentValidation;

namespace Restaurants.Application.Carts.Commands;

public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
{
    public UpdateCartItemCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);

        RuleFor(c => c.Quantity)
            .GreaterThan(0);
    }
}
