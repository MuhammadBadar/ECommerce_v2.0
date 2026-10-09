using Ordering.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class Order //Aggregate Root
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; } //DDD Concept
        public OrderStatus Status { get; private set; }

        private List<OrderItem> Items = new List<OrderItem>(); //Aggregate
        public Order() 
        { 
            // For Entiry Framework
        }

        public void AddItem()
        {

        }

        public void RemoveItem()
        {

        }

        public void Cancel()
        {
            // Rule for Cancellation i.e. Rule:6 
            if (Status == OrderStatus.Completed)
                throw new Exception("Completed orders can't be cancelled.");

            Status = OrderStatus.Cancelled;
        }

        public void Place()
        {
            if (Status != OrderStatus.Draft)
                throw new Exception("Only draft orders can be placed.");

            if (Items.Count == 0)
                throw new Exception("An order must contain atleast on item");

            Status = OrderStatus.Placed;
        }

        private Order(Guid customerId)
        {
            // Rule 1
            if (customerId == Guid.Empty)
            {
                var ex = new ArgumentException("Customer is Required", "customerId");
            }
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Status = OrderStatus.Draft;
        }



        // Factory
        public static Order Create(
            Guid customerId
            )
        {

            return new Order(customerId);
        }

        // Create Order
    }
}
