using AiOption.Domain.Common;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class CreateTokenSuccess : AggregateEvent<CustomerAggregate, CustomerId>{
        public Token Token { get; }
        public CreateTokenSuccess(Token token) 
        {
            Token = token;
        }

    }
}