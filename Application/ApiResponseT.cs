using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public static ApiResponse<T> Success(T? data, string message = "Success", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }
    }
}
