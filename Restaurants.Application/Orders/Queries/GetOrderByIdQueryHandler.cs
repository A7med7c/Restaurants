using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Queries;

public class GetOrderByIdQueryHandler(
    ILogger<GetOrderByIdQuery> logger,
    IMapper mapper,
    IOrdersRepository ordersRepository) : IRequestHandler<GetOrderByIdQuery, OrdersDto>
{
    public async Task<OrdersDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get order with id: {id}", request.Id);

        var order = await ordersRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Order), request.Id.ToString());
       
        var ordersDto = mapper.Map<OrdersDto>(order);
        return ordersDto;
    }
}
