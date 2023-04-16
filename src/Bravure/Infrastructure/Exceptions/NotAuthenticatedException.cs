using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bravure.Infrastructure.Exceptions
{

    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException()
            : base("The user is not authenticated")
        {
        }
    }
}
