namespace Formulario_Cadastro_Cliente.Services
{
    public class AppServiceResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public T Data { get; private set; }

        private AppServiceResult(bool isSuccess, T data, string errorMessage)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static AppServiceResult<T> Success(T data)
        {
            return new AppServiceResult<T>(true, data, null);
        }

        public static AppServiceResult<T> Failure(string errorMessage)
        {
            return new AppServiceResult<T>(false, default(T), errorMessage);
        }
    }
}