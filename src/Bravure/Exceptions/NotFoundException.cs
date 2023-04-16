using System;
using System.Net;

namespace Bravure.Exceptions
{
    public class NotFoundException : BaseHttpException
    {
        protected NotFoundException() : base()
        {
            base.HttpStatusCode = HttpStatusCode.NotFound;
        }

        public NotFoundException(string message) : base(message)
        {
            base.HttpStatusCode = HttpStatusCode.NotFound;
        }

        public NotFoundException(string type, int id) : base(GetMessage(type, id))
        {
            base.HttpStatusCode = HttpStatusCode.NotFound;
        }
        public NotFoundException(string type, Guid id) : base(GetMessage(type, id))
        {
            base.HttpStatusCode = HttpStatusCode.NotFound;
        }
    }

    public class NotFoundException<T1> : NotFoundException
    {
        protected NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string type, T1 id) : base(GetMessage(type, id))
        {
        }
    }

    public class NotFoundException<T1, T2> : NotFoundException<T1>
    {
        public NotFoundException(string type, string id1Message, T1 id1, string id2Message, T2 id2)
            : base(GetMessage(type, id1Message, id1, id2Message, id2))
        {
        }
    }
}
