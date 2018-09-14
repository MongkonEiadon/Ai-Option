﻿using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events {

    public class CustomerRegisterSucceeded : AggregateEvent<CustomerAggregate, CustomerId> {

        public CustomerState NewCustomer { get; }

        protected CustomerRegisterSucceeded(CustomerState newCustomer) {
            NewCustomer = newCustomer;
        }

    }

}