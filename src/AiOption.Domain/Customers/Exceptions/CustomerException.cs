using System;
using System.Collections.Generic;
using System.Text;

namespace AiOption.Domain.Customers
{
    public class CustomerException : Exception {

        public Guid CustomerId { get; }

        public CustomerException(Guid customerId, string message) : base(message) {
            CustomerId = customerId;
        }
    }
}
