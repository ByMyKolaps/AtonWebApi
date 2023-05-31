namespace AtonWebApi.DAL.DTO
{
    public class Maybe
    {
        public readonly bool IsSuccess;
        public readonly string? ErrorMessage;

        public Maybe(bool isSuccess, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = isSuccess ? null : errorMessage;
        }
    }
}
