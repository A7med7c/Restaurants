using AutoMapper;
using Restaurants.Application.Dishes.Commands;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Dtos;
public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishesDto>();

        CreateMap<CreatDishCommand, Dish>();
    }
}
