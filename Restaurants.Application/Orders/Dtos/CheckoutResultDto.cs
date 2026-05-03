namespace Restaurants.Application.Orders.Dtos;

public class CheckoutResultDto
{
    public int OrderId { get; set; }
    public string? PaymentUrl { get; set; }
}
