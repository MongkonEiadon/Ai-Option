using System;
using System.Collections.Generic;
using System.Text;
using AiOption.Domain.Common;
using EventFlow.Aggregates;

namespace AiOption.Domain.IqOptions.Events
{
    public class IqAccountLoginEvent : AggregateEvent<IqAggregate, IqId> {
        public User UserName { get; }
        public Password Password { get; }

        public IqAccountLoginEvent(User userName, Password password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
