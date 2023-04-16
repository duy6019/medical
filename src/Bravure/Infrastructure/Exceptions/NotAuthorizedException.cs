using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bravure.Infrastructure.Exceptions
{

    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException()
            : base("The current user is not authorized to perform this action")
        {
        }

        public NotAuthorizedException(string currentUserId, string actionName, object entity)
            : base($"The user '{currentUserId}' is not authorized to perform {actionName}, on : {entity}")
        {
        }
    }
}
