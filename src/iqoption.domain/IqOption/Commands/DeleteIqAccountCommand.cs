using System;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace iqoption.domain.IqOption.Command {
    public class DeleteIqAccountResult : IExecutionResult {
        public DeleteIqAccountResult(bool isSuccess) {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
    }

    public class DeleteIqAccountCommand : Command<IqOptionAggregate, IqOptionIdentity, DeleteIqAccountResult> {
        public DeleteIqAccountCommand(IqOptionIdentity id, Guid iqAccountId) : base(id) {
            IqAccountId = iqAccountId;
        }

        public Guid IqAccountId { get; }
    }
}