using AutoMapper;
using Restaurants.Application.MenuCategories.Commands.CreateMenuForRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.MenuCategories.Dtos;

public class MenuCategoriesProfile : Profile
{
    public MenuCategoriesProfile()
    {
        CreateMap<MenuCategory, MenuCategoriesDto>()
            .ForMember(d => d.Dishes, opt
            => opt.MapFrom(src => src.Dishes));

        CreateMap<CreateMenuForRestaurantCommand, MenuCategory>();
    }
}
