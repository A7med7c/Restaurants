using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    private readonly List<string> validcategory = ["Italian", "Mexican", "Japanese", "American", "Indian"];

    public UpdateRestaurantCommandValidator()
    {
        RuleFor(c => c.Name)
            .Length(3, 100);

        RuleFor(x => x.Category)
              .Must(validcategory.Contains);
    }
}
