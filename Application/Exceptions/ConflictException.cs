using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ConflictException : ApiException
    {
        public ConflictException(string message) : base(message, 409)
        {
        }
    }
}
