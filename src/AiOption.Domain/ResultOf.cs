namespace AiOption.Domain {

    public class ResultOf<T> : BaseResult {

        public T Result { get; }

        public ResultOf(bool isSuccess, T result) : base(isSuccess) {
            Result = result;
        }

    }

}