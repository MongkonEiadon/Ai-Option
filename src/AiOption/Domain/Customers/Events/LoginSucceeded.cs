using System;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class LoginSucceeded : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public DateTimeOffset SuccessTime { get; }

        public LoginSucceeded(DateTimeOffset successTime)
        {
            SuccessTime = successTime;
        }
    }
}