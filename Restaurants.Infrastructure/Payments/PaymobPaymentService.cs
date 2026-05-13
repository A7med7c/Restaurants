using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Restaurants.Application.Payments;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Configurations;

namespace Restaurants.Infrastructure.Payments;

internal class PaymobPaymentService : IPaymentService
{
    private const string Currency = "EGP";
    private readonly HttpClient _httpClient;
    private readonly PaymobSettings _settings;

    public PaymobPaymentService(HttpClient httpClient, IOptions<PaymobSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;

        Console.WriteLine($"Paymob ApiKey: {_settings.ApiKey}");

        if (string.IsNullOrWhiteSpace(_settings.ApiKey))
        {
            throw new Exception("Paymob ApiKey is not configured.");
        }
    }

    public async Task<string> CreatePaymentAsync(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        if (order.Items == null || order.Items.Count == 0)
        {
            throw new InvalidOperationException("Order items must not be empty.");
        }

        ValidateSettings(_settings);

        _httpClient.BaseAddress ??= new Uri("https://accept.paymob.com");

        var authToken = await AuthenticateAsync(_settings.ApiKey);
        var paymobOrderId = await CreateOrderAsync(authToken, order);
        var paymentToken = await GeneratePaymentKeyAsync(authToken, paymobOrderId, order, _settings.IntegrationId);

        return $"https://accept.paymob.com/api/acceptance/iframes/{_settings.IframeId}?payment_token={paymentToken}";
    }

    private async Task<string> AuthenticateAsync(string apiKey)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/auth/tokens", new { api_key = apiKey });
        var auth = await ReadResponseAsync<AuthTokenResponse>(response, "Paymob auth failed");
        return auth.Token;
    }

    private async Task<int> CreateOrderAsync(string authToken, Order order)
    {
        var request = new CreateOrderRequest
        {
            AuthToken = authToken,
            DeliveryNeeded = false,
            AmountCents = GetAmountCents(order),
            Currency = Currency,
            MerchantOrderId = order.Id,
            Items = order.Items.Select(item => new PaymobItem
            {
                Name = item.Name,
                AmountCents = (int)Math.Round(item.Price * 100m),
                Quantity = item.Quantity
            }).ToList()
        };

        var response = await _httpClient.PostAsJsonAsync("/api/ecommerce/orders", request);
        var created = await ReadResponseAsync<CreateOrderResponse>(response, "Paymob order creation failed");
        return created.Id;
    }

    private async Task<string> GeneratePaymentKeyAsync(string authToken, int paymobOrderId, Order order, int integrationId)
    {
        var request = new PaymentKeyRequest
        {
            AuthToken = authToken,
            AmountCents = GetAmountCents(order),
            Expiration = 3600,
            OrderId = paymobOrderId,
            Currency = Currency,
            IntegrationId = integrationId,
            BillingData = new BillingData
            {
                FirstName = "NA",
                LastName = "NA",
                Email = "na@example.com",
                PhoneNumber = "0000000000",
                Apartment = "NA",
                Floor = "NA",
                Street = "NA",
                Building = "NA",
                City = "NA",
                Country = "NA",
                State = "NA",
                ZipCode = "00000"
            }
        };

        var response = await _httpClient.PostAsJsonAsync("/api/acceptance/payment_keys", request);
        var key = await ReadResponseAsync<PaymentKeyResponse>(response, "Paymob payment key generation failed");
        return key.Token;
    }

    private static int GetAmountCents(Order order)
    {
        var totalPrice = order.Items.Sum(i => i.Price * i.Quantity);
        if (totalPrice <= 0)
        {
            throw new InvalidOperationException("Order total price must be greater than zero.");
        }

        return (int)Math.Round(totalPrice * 100m);
    }

    private static void ValidateSettings(PaymobSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.ApiKey))
        {
            throw new InvalidOperationException("Paymob ApiKey is not configured.");
        }

        if (settings.IntegrationId <= 0)
        {
            throw new InvalidOperationException("Paymob IntegrationId is not configured.");
        }

        if (settings.IframeId <= 0)
        {
            throw new InvalidOperationException("Paymob IframeId is not configured.");
        }
    }

    private static async Task<T> ReadResponseAsync<T>(HttpResponseMessage response, string errorMessage)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"{errorMessage}. Status: {(int)response.StatusCode} ({response.StatusCode}). Body: {errorBody}");
        }

        var payload = await response.Content.ReadFromJsonAsync<T>();
        if (payload == null)
        {
            throw new HttpRequestException($"{errorMessage}. Empty response.");
        }

        return payload;
    }

    private sealed class AuthTokenResponse
    {
        public string Token { get; set; } = string.Empty;
    }

    private sealed class CreateOrderResponse
    {
        public int Id { get; set; }
    }

    private sealed class PaymentKeyResponse
    {
        public string Token { get; set; } = string.Empty;
    }

    private sealed class CreateOrderRequest
    {
        public string AuthToken { get; set; } = string.Empty;
        public bool DeliveryNeeded { get; set; }
        public int AmountCents { get; set; }
        public string Currency { get; set; } = string.Empty;
        public int MerchantOrderId { get; set; }
        public List<PaymobItem> Items { get; set; } = new();
    }

    private sealed class PaymentKeyRequest
    {
        public string AuthToken { get; set; } = string.Empty;
        public int AmountCents { get; set; }
        public int Expiration { get; set; }
        public int OrderId { get; set; }
        public string Currency { get; set; } = string.Empty;
        public int IntegrationId { get; set; }
        public BillingData BillingData { get; set; } = new();
    }

    private sealed class BillingData
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Apartment { get; set; } = string.Empty;
        public string Floor { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Building { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }

    private sealed class PaymobItem
    {
        public string Name { get; set; } = string.Empty;
        public int AmountCents { get; set; }
        public int Quantity { get; set; }
    }
}
