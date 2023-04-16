using System.Net;

namespace Bravure.Exceptions
{
    public class BadRequestException : BaseHttpException
    {
        public BadRequestException(string message) : base(message)
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
        }
    }
}
