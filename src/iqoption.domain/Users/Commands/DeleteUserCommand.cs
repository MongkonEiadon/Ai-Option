using System;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace iqoption.domain.Users.Commands {
    public class DeleteUserResult : IExecutionResult {
        public DeleteUserResult(bool isSuccess, string message = "") {
            IsSuccess = isSuccess;
            Message = message;
        }

        public string Message { get; }
        public bool IsSuccess { get; }
    }

    public class DeleteUserCommand : Command<UserAggregrate, UserIdentity, DeleteUserResult> {
        public DeleteUserCommand(UserIdentity id, string userId) : base(id) {
            Id = userId;
        }

        public string Id { get; }
    }
}