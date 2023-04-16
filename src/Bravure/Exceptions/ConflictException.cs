using System.Net;

namespace Bravure.Exceptions
{
    public class ConflictException : BaseHttpException
    {
        public ConflictException(string message) : base(message)
        {
            HttpStatusCode = HttpStatusCode.Conflict;
        }
    }
}
