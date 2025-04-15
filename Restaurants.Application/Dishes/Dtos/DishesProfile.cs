using AutoMapper;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Dtos;
public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishesDto>();
    }
}
