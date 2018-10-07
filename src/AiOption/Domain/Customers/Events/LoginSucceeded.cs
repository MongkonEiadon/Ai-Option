using System;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class LoginSucceeded : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public LoginSucceeded(DateTimeOffset successTime)
        {
            SuccessTime = successTime;
        }

        public DateTimeOffset SuccessTime { get; }
    }
}