using System;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace iqoption.domain.IqOption.Command {
   

    public class DeleteIqAccountCommand : Command<IqAggregate, IqIdentity, CommandResult> {
        public DeleteIqAccountCommand(IqIdentity id, Guid iqAccountId) : base(id) {
            IqAccountId = iqAccountId;
        }

        public Guid IqAccountId { get; }
    }
}