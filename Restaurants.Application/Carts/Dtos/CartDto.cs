namespace Restaurants.Application.Carts.Dtos;

public class CartDto
{
    public int Id { get; set; }
    public ICollection<CartItemDto> Items { get; set; } = new List<CartItemDto>();
}
