using Restaurants.Domain.Entities;

namespace Restaurants.Application.MenuCategories.Dtos;

public class MenuCategoriesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<Dish> Dishes { get; set; } = new();

}
