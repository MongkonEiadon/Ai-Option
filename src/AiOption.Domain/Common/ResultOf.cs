using AiOption.Domain.Customers;
using AiOption.Domain.Customers.Commands;

using EventFlow.Commands;

namespace AiOption.Domain.Common {

    public class ResultOf<T> : BaseResult {

        public ResultOf(bool isSuccess, T result) : base(isSuccess) {
            Result = result;
        }

        public ResultOf(bool isSuccess, string message, T result = default(T)) : this(isSuccess, result) {
            Message = message;
        }

        public T Result { get; }

    }

}