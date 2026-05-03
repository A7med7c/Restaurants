using MediatR;

namespace Restaurants.Application.Carts.Commands;

public class UpdateCartItemCommand : IRequest
{
    public int Id { get; set; }
    public int Quantity { get; set; }
}
