using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bravure.Infrastructure.Exceptions
{
    public class KdmsException : Exception
    {
        public KdmsException(string endpoint, string message)
            : base($"KDMS error for request ({endpoint}) with error: {message}")
        {
        }
    }
}
