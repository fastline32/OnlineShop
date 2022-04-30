using System.Collections.Generic;

namespace OnlineShop.Errors
{
    public class ApiValidationError : ApiResponse
    {
        public ApiValidationError() : base(400)
        {
        }

        public IEnumerable<string> Error { get; set; }
    }
}