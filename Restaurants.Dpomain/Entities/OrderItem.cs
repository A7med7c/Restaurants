﻿using System.Text.Json.Serialization;

namespace Restaurants.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int OrderId { get; set; }
    [JsonIgnore]
    public Order Order { get; set; } = default!;
}
