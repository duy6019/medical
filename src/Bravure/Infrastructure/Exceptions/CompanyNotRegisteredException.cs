using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bravure.Infrastructure.Exceptions
{

    public class CompanyNotRegisteredException : Exception
    {
        public CompanyNotRegisteredException(string message)
            : base(message)
        {
        }
    }
}
