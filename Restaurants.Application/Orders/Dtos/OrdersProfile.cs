using AutoMapper;
using Restaurants.Application.Orders.Commands;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Orders.Dtos;

public class OrdersProfile : Profile
{
    public OrdersProfile()
    {
        CreateMap<CreateOrderCommand, Order>();
    }
}
