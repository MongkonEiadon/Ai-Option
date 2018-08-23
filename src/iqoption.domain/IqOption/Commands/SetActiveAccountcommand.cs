using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;

namespace iqoption.domain.IqOption.Commands {

    public class SetActiveAccountcommand : Command<IqAggregate, IqIdentity, CommandResult> {

        public SetActiveAccountStatusItem StatusItem { get; }

        public SetActiveAccountcommand(IqIdentity aggregateId, SetActiveAccountStatusItem statusItem) : base(aggregateId) {
            StatusItem = statusItem;
        }

       
    }
}