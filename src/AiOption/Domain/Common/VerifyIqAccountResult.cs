using EventFlow.Aggregates.ExecutionResults;

namespace AiOption.Domain.Common
{
    public class VerifyIqAccountResult : IExecutionResult
    {
        public VerifyIqAccountResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public string Message { get; }
        public bool IsSuccess { get; }
    }
}