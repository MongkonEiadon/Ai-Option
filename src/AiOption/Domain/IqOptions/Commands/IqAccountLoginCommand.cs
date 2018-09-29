using System;
using System.Collections.Generic;
using System.Text;
using AiOption.Domain.Common;
using EventFlow.Commands;
using EventFlow.Core;

namespace AiOption.Domain.IqOptions.Commands
{
    public class IqAccountLoginCommand : Command<IqAggregate, IqId>
    {
        public User UserName { get; }
        public Password Password { get; }

        public IqAccountLoginCommand(IqId aggregateId, string userName, string password) : base(aggregateId)
        {
            UserName = new User(userName);
            Password = new Password(password);
        }
        
    }
}
