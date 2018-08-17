using EventFlow.Aggregates.ExecutionResults;

namespace AiOption.Domain {

    public class BaseResult : IExecutionResult {

        private readonly bool _isSuccess;

        public bool IsSuccess { get; }

        public BaseResult(bool isSuccess) {
            _isSuccess = isSuccess;
        }

    }

}