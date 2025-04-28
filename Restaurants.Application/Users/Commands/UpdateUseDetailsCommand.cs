using MediatR;

namespace Restaurants.Application.Users.Commands;

public class UpdateUseDetailsCommand : IRequest
{
    public string? Nationality { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}
