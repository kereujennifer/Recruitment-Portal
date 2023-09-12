namespace Auth.Services
{
    public class ReturnData<T> 
    {
        public ReturnData()
        {
            Success = true;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}