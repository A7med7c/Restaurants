namespace Restaurants.Api.Contracts.Payments;

public class PaymobCallbackRequest
{
    public int OrderId { get; set; }
    public bool Success { get; set; }
}
