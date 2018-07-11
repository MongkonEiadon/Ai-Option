using EventFlow.Aggregates.ExecutionResults;

namespace iqoption.domain.Users.Commands.Results {
    public class ChangeUserRoleCommandResult : IExecutionResult
    {
        private readonly string _message;

        public ChangeUserRoleCommandResult(bool isSuccess, string message) {
            _message = message;
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
        public string Message { get; }
    }
}