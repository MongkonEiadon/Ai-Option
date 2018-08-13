using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;

namespace iqoption.domain.IqOption.Commands {

    public class SetActiveAccountcommand : Command<IqAggregate, IqIdentity, CommandResult> {

        public ActiveAccountItem Item { get; }

        public SetActiveAccountcommand(IqIdentity aggregateId, ActiveAccountItem item) : base(aggregateId) {
            Item = item;
        }

       
    }
}