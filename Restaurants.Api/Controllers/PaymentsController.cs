using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Api.Contracts.Payments;
using Restaurants.Application.Payments.Commands;
using Restaurants.Domain.Constants;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("callback")]
        public async Task<IActionResult> HandlePaymobCallback([FromBody] PaymobCallbackRequest request)
        {
            var status = request.Success ? PaymentStatus.Paid : PaymentStatus.Failed;

            var command = new HandlePaymobCallbackCommand
            {
                OrderId = request.OrderId,
                PaymentStatus = status
            };

            await mediator.Send(command);
            return Ok();
        }
    }
}
