using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Carts.Dtos;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartItem, CartItemDto>();
        CreateMap<Cart, CartDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
