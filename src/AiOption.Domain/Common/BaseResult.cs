using EventFlow.Aggregates.ExecutionResults;

namespace AiOption.Domain.Common {

    public class BaseResult : IExecutionResult {


        public BaseResult(bool isSuccess, string message = null) {
            IsSuccess = isSuccess;
            Message = message;
        }

        public virtual string Message { get; protected set; }


        public bool IsSuccess { get; }

    }

}