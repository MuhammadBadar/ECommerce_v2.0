using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Ordering.Application.Orders.Commands.PlaceOrder;

public sealed record PlaceOrderCommand(
    Guid CustomerId,
    string AddressLine,
    string City,
    string PostalCode,
    string Country,
    IReadOnlyCollection<PlaceOrderItem> Items
) : IRequest<PlaceOrderResponse>;

public sealed record PlaceOrderItem(
    Guid ProductId,
    decimal UnitPrice,
    int Quantity
);