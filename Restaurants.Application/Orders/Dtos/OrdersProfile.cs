using AutoMapper;
using Restaurants.Application.Orders.Commands;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Orders.Dtos;

public class OrdersProfile : Profile
{
    public OrdersProfile()
    {
         // Map CreateOrderCommand to Order
            CreateMap<CreateOrderCommand, Order>()
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore()) 
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Map OrderItemDto to OrderItem
        CreateMap<OrderItemDto, OrderItem>();
        
        // Map Order to OrderItem
        CreateMap<Order,OrdersDto>()
             .ForMember(dest => dest.TotalAmount, opt =>
             opt.MapFrom(src => src.TotalAmount))
                .ForMember(o => o.Items, opt =>
                opt.MapFrom(src => src.Items));
    }
}
