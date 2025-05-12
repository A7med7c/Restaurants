
namespace Restaurants.Application.Dishes.Dtos;

public class DishesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public int RestaurantId { get; set; }

    //Manual Mapping
    //public static DishesDto FromEntity(Dish dish)
    //{
    //    return new DishesDto
    //    {
    //        Description = dish.Description,
    //        Name = dish.Name,
    //        Id = dish.Id,
    //        Price = dish.Price, 
    //        KiloCalories = dish.KiloCalories
    //    };
    //}
}
