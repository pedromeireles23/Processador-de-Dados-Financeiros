namespace FinanceProcessor.Core.Common
{
    public record Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }

        private Result(bool isSuccess, T? value, string? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T valor)
        {
            return new Result<T>(true, valor, null);
        }

        public static Result<T> Failure(string failure)
        {
            return new Result<T>(false, default, failure);
        }
    }
}
