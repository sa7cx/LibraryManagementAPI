using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message) : base(message, 400)
        {
        }
    }
}
