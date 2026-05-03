namespace Restaurants.Application.Carts.Dtos;

public class CartItemDto
{
    public int Id { get; set; }
    public int DishId { get; set; }
    public int Quantity { get; set; }
}
