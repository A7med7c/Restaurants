using MediatR;

namespace Restaurants.Application.Carts.Commands;

public class AddToCartCommand : IRequest
{
    public int DishId { get; set; }
    public int Quantity { get; set; }
}
