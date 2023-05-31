namespace AtonWebApi.DAL
{
    public class TMaybe<T>
    {
        public readonly T? Value;
        public readonly bool IsSuccess;
        public readonly string ErrorMessage;
        public TMaybe(T value)
        {
            Value = value;
            IsSuccess = true;
            ErrorMessage = string.Empty;
        }

        public TMaybe(string errorMessage)
        {
            Value = default;
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }
}
