﻿using Restaurants.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    [NotMapped]
    public decimal TotalAmount { get; private set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public string CustomerId { get; set; } = default!;
    public User Customer { get; set; } = default!;

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
        TotalAmount += item.Price * item.Quantity;
    }

}
