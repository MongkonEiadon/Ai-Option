using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;

namespace iqoption.domain.IqOption.Commands
{

    public class SetActiveAccountResult : IExecutionResult
    {
        public bool IsSuccess { get; }

        public SetActiveAccountResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class SetActiveAccountcommand : Command<IqOptionAggregate, IqOptionIdentity, SetActiveAccountResult> {

        public ActiveAccountItem Item { get; }

        public SetActiveAccountcommand(IqOptionIdentity aggregateId, ActiveAccountItem item) : base(aggregateId) {
            Item = item;
        }

       
    }
}