using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public class Order
{
    private readonly List<OrderItem> _items = new();

    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public OrderStatus Status { get; private set; }

    public Address ShippingAddress { get; private set; } = null!;

    public IReadOnlyCollection<OrderItem> Items =>
        _items.AsReadOnly();

    private Order()
    {
        // Required by EF Core later.
    }
    private Order(
        Guid customerId,
        Address shippingAddress)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException(
                "Customer is required.",
                nameof(customerId));

        Id = Guid.NewGuid();
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
        Status = OrderStatus.Draft;
    }

    public static Order Create(
        Guid customerId,
        Address shippingAddress)
    {
        return new Order(
            customerId,
            shippingAddress);
    }

    public void AddItem(
        Guid productId,
        decimal unitPrice,
        int quantity)
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException(
                "Items can only be added to a draft order.");

        var existingItem = _items
            .FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.ChangeQuantity(
                existingItem.Quantity + quantity);

            return;
        }

        var item = new OrderItem(
            productId,
            unitPrice,
            quantity);

        _items.Add(item);
    }

    public void RemoveItem(Guid productId)
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException(
                "Items can only be removed from a draft order.");

        var item = _items
            .FirstOrDefault(x => x.ProductId == productId);

        if (item == null)
            return;

        _items.Remove(item);
    }

    public decimal GetTotalAmount()
    {
        return _items.Sum(x => x.GetTotal());
    }

    public void Place()
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException(
                "Only draft orders can be placed.");

        if (!_items.Any())
            throw new InvalidOperationException(
                "An order must contain at least one item.");

        Status = OrderStatus.Placed;
    }
    public void Cancel()
    {
        if (Status == OrderStatus.Completed)
            throw new InvalidOperationException(
                "Completed orders cannot be cancelled.");

        if (Status == OrderStatus.Cancelled)
            return;

        Status = OrderStatus.Cancelled;
    }
}
