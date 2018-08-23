using EventFlow.Aggregates.ExecutionResults;

namespace iqoption.domain {
    public class CommandResult : IExecutionResult {
        public bool IsSuccess { get; }
        public CommandResult(bool isSuccess) {
            IsSuccess = isSuccess;
        }
    }

    public class NotSuccessResult : CommandResult {
        public NotSuccessResult() : base(false) { }
        public static CommandResult New => new NotSuccessResult();
    }

    public class SuccessResult : CommandResult {
        public SuccessResult() : base(true) { }
        public static CommandResult New => new SuccessResult();
    }

    public class CommandResult<TResult> : CommandResult {
        public TResult Result { get; }

        public CommandResult(bool isSccess, TResult result):base(isSccess) {
            Result = result;
        }
        
    }
}