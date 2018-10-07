using AiOption.Domain.Common;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class CreateTokenSuccess : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public CreateTokenSuccess(Token token)
        {
            Token = token;
        }

        public Token Token { get; }
    }
}