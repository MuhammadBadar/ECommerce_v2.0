using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Enums;

public enum OrderStatus
{
    Draft = 1,
    Placed = 2,
    Confirmed = 3,
    Cancelled = 4,
    Completed = 5
}