using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandValidator : AbstractValidator<CreateDishForRestaurantCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(d => d.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price shouldn't be nwgative");

        RuleFor(d => d.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("KiloCalories shouldn't be nwgative");
           
    }
}
