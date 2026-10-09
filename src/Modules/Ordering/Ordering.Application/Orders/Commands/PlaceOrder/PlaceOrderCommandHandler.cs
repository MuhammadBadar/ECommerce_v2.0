using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Ordering.Application.Abstractions.Persistence;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.PlaceOrder;

public sealed class PlaceOrderCommandHandler
    : IRequestHandler<PlaceOrderCommand, PlaceOrderResponse>
{
    private readonly IOrderRepository _orderRepository;

    public PlaceOrderCommandHandler(
        IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<PlaceOrderResponse> Handle(
        PlaceOrderCommand request,
        CancellationToken cancellationToken)
    {
        var address = Address.Create(
            request.AddressLine,
            request.City,
            request.PostalCode,
            request.Country);

        var order = Order.Create(
            request.CustomerId,
            address);

        foreach (var item in request.Items)
        {
            order.AddItem(
                item.ProductId,
                item.UnitPrice,
                item.Quantity);
        }

        order.Place();

        await _orderRepository.AddAsync(
            order,
            cancellationToken);

        return new PlaceOrderResponse(
            order.Id,
            order.GetTotalAmount(),
            order.Status.ToString());
    }
}