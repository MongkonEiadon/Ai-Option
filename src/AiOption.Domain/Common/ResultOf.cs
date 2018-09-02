namespace AiOption.Domain.Common {

    public class ResultOf<T> : BaseResult {

        public ResultOf(bool isSuccess, T result) : base(isSuccess) {
            Result = result;
        }

        public T Result { get; }

    }

}