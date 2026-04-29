namespace Restaurants.Domain.Entities;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}
