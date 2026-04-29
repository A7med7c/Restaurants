using FluentValidation;

namespace Restaurants.Application.Carts.Commands;

public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
    public AddToCartCommandValidator()
    {
        RuleFor(c => c.DishId)
            .GreaterThan(0);

        RuleFor(c => c.Quantity)
            .GreaterThan(0);
    }
}
