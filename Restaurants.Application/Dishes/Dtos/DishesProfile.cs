using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos;
public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishesDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());


        CreateMap<CreateDishInMenuForRestaurantCommand, Dish>();

        CreateMap<UpdateDishInMenuForRestaurantCommand, Dish>();
     

    }

}
