namespace Restaurants.Infrastructure.Payments;

public class PaymobSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public int IntegrationId { get; set; } = 123456;
    public int IframeId { get; set; } = 6789;
}
