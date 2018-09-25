using System;

namespace AiOption.Domain.Customers {

    public class CustomerException : Exception {

        public CustomerException(Guid customerId, string message) : base(message) {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }

    }

}