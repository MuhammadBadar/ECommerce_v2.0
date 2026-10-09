using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; } // ProductType should not be there
        public int Quantity { get; private set; }

        //Ubiquotous Language (i.e. Language of Business)
        public decimal UnitPrice { get; private set; }

        public OrderItem() { }
        public OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            // Validation Rule should be applied here
            
            // Rule 2
            if(quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            // Rule 7
            if(productId == Guid.Empty)
                throw new ArgumentException("Product is required.", nameof(productId));

            if (unitPrice < 0)
                throw new Exception("Unit Price cannot be negative");

            Id = Guid.NewGuid();
        }
    }
}
