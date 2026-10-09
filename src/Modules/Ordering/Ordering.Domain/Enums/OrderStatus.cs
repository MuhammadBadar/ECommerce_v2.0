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

    //Design Pattern: The Best (Current Best) Solution of a Recurring Problem 
    //

}

// CRM
public enum InquiryStatus
{
    New,
    InProgress,
    Hold,
    Win,
    Lose
}

public class Test
{
    public Test()
    {
       throw new ArgumentException("TestMsg", nameof(Test));
    }
}

