using System;
using System.Collections.Generic;
using System.Text;
using AiOption.Domain.Common;
using AiOption.Domain.IqOptions.Events;
using EventFlow.Aggregates;

namespace AiOption.Domain.IqOptions
{
    public class IqAggregateState : AggregateState<IqAggregate, IqId, IqAggregateState>
    {

    }

    public class IqAggregate : AggregateRoot<IqAggregate, IqId>
    {
        private IqAggregateState state { get; } = new IqAggregateState();
        public IqAggregate(IqId id) : base(id)
        {
            Register(state);
        }

        public void RequestLogin(User userName, Password password)
        {
            Emit(new IqAccountLoginEvent(userName, password));
        }

        public void LoginSucceeded() { }
        public void LoginFailed(string failedMessage) { }
    }
}
