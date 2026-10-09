using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException(
                "Amount cannot be negative.",
                nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException(
                "Currency is required.",
                nameof(currency));

        Amount = amount;
        Currency = currency;
    }

    public static Money Create(
        decimal amount,
        string currency)
    {
        return new Money(amount, currency);
    }
}
