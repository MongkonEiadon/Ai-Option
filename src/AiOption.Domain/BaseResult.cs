using EventFlow.Aggregates.ExecutionResults;

namespace AiOption.Domain {

    public class BaseResult : IExecutionResult {


        public bool IsSuccess { get; }
        public string Message { get; protected set; }


        public BaseResult(bool isSuccess, string message = null) {
            IsSuccess = isSuccess;
            Message = message;
        }

    }

}