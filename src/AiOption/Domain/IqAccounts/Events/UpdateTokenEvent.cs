using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts.Events
{
    public class UpdateTokenEvent : AggregateEvent<IqAccountAggregate, IqAccountId>
    {
        public string Token { get; }

        public UpdateTokenEvent(string token)
        {
            Token = token;
        }
    }
}
