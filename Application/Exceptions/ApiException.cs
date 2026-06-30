using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ApiException :Exception
    {
        public int StatusCode { get;}
        public ApiException(string message , int StatusCode):base(message)
        {
            this.StatusCode = StatusCode;
        }
    }
}
