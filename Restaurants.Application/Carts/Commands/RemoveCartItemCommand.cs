using MediatR;

namespace Restaurants.Application.Carts.Commands;

public class RemoveCartItemCommand : IRequest
{
    public int Id { get; set; }
}
