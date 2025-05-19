namespace Restaurants.Application.Orders.Dtos;

public class OrderItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public int OrderId { get; set; }
}
