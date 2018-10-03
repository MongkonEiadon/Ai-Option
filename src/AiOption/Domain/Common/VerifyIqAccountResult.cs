using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates.ExecutionResults;

namespace AiOption.Domain.Common
{
    public class VerifyIqAccountResult : IExecutionResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        public VerifyIqAccountResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
