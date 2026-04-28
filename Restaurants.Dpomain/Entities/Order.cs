using Restaurants.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
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

    public void UpdateStatus(OrderStatus newStatus)
    {
        if (newStatus == Status)
        {
            return;
        }

        if (!IsValidTransition(newStatus))
        {
            throw new InvalidOperationException($"Invalid status transition: {Status} -> {newStatus}");
        }

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    private bool IsValidTransition(OrderStatus newStatus)
    {
        return Status switch
        {
            OrderStatus.Pending => newStatus is OrderStatus.Confirmed or OrderStatus.Cancelled,
            OrderStatus.Confirmed => newStatus is OrderStatus.Preparing or OrderStatus.Cancelled,
            OrderStatus.Preparing => newStatus is OrderStatus.Ready or OrderStatus.Cancelled,
            OrderStatus.Ready => newStatus is OrderStatus.OnTheWay or OrderStatus.Cancelled,
            OrderStatus.OnTheWay => newStatus is OrderStatus.Delivered or OrderStatus.Cancelled,
            OrderStatus.Delivered => false,
            OrderStatus.Cancelled => false,
            _ => false
        };
    }

}
