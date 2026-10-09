using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders.Commands.PlaceOrder;

namespace Ordering.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(
        PlaceOrderCommand command,
        CancellationToken cancellationToken
        )
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}