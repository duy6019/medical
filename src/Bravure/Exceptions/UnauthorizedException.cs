using System.Net;

namespace Bravure.Exceptions
{
    public class UnauthorizedException : BaseHttpException
    {
        public UnauthorizedException(string message) : base(message)
        {
            HttpStatusCode = HttpStatusCode.Unauthorized;
        }
    }
}
