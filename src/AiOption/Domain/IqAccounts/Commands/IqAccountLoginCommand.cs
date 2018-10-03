using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AiOption.Domain.Common;
using EventFlow.Commands;
using EventFlow.Core;

namespace AiOption.Domain.IqAccounts.Commands
{
    public class IqAccountLoginCommand : Command<IqAccountAggregate, IqAccountId>
    {
        public IqAccountLoginCommand(IqAccountId aggregateAccountId) : base(aggregateAccountId)
        {
        }
    }
}
