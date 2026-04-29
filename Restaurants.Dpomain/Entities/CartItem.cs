namespace Restaurants.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; } = default!;
    public int DishId { get; set; }
    public Dish Dish { get; set; } = default!;
    public int Quantity { get; set; }
}
