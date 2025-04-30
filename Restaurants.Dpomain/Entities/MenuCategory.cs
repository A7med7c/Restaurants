namespace Restaurants.Domain.Entities;

public class MenuCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int RestaurantId { get; set; }
    public List<Dish> Dishes { get; set; } = new();
}
