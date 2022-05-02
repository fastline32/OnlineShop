namespace OnlineShop.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request for you",
                401 => "Need valid authentication",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}