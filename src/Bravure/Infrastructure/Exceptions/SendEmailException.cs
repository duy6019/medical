using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bravure.Infrastructure.Exceptions
{
    public class SendEmailException : Exception
    {
        public SendEmailException(string email, string message)
            : base($"Failed to send email to ({email}) with error: {message}")
        {
        }
    }
}
