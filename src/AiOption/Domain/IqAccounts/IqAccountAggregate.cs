using System;
using System.Collections.Generic;
using System.Text;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts.Events;
using EventFlow.Aggregates;

namespace AiOption.Domain.IqAccounts
{
    public class IqAccountAggregate : AggregateRoot<IqAccountAggregate, IqAccountId>
    {
        private IqAggregateState state { get; } = new IqAggregateState();

        public IqAccountAggregate(IqAccountId accountId) : base(accountId)
        {
            Register(state);
        }

        public void ChangeToken(string token) => Emit(new UpdateTokenEvent(token));

        public void LoginSucceeded()
        {
        }

        public void LoginFailed(string failedMessage)
        {
        }
    }
}
