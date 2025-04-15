using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public List<DishesDto> Dishes { get; set; } = new();

    //Manual Mapping
    //public static RestaurantDto? FromEntity(Restaurant? restaurant)
    //{
    //    if (restaurant is null)
    //        return null;
    //    return new RestaurantDto
    //    {
    //        Category = restaurant.Category,
    //        Name = restaurant.Name,
    //        Description = restaurant.Description,
    //        HasDelivery = restaurant.HasDelivery,
    //        Id = restaurant.Id,
    //        City = restaurant.Address?.City,
    //        PostalCode = restaurant.Address?.PostalCode,
    //        Street = restaurant.Address?.Street,
    //        Dishes = restaurant.Dishes.Select(Dish => DishesDto.FromEntity(Dish)).ToList()
    //    };
    //}
}
