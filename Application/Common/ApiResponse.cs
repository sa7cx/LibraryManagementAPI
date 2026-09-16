using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public static ApiResponse Success(string message = "Success", int statusCode = 200)
        {
            return new ApiResponse
            {
                StatusCode = statusCode,
                Message = message,
            };
        }
        public static ApiResponse Fail(string message,int statusCode)
        {
            return new ApiResponse
            {
                StatusCode = statusCode,
                Message = message,
            };
        }
    }
}
