using System;
using System.Net;

namespace Bravure.Exceptions
{
    public class BaseHttpException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        protected BaseHttpException() : base()
        {
        }

        public BaseHttpException(string message) : base(message)
        {
        }

        public BaseHttpException(string type, int id) : base(GetMessage(type, id))
        {
        }

        protected static string GetMessage(string type, int id) => $"{type} with id: {id} not found";

        protected static string GetMessage<T1>(string type, T1 id) => $"{type} with id: {id} not found";

        protected static string GetMessage<T1, T2>(string type, string id1Message, T1 id1, string id2Message, T2 id2) => $"{type} with {id1Message}: {id1} and {id2Message}: {id2} not found";
    }

    public class BaseHttpException<T1> : BaseHttpException
    {
        protected BaseHttpException(string message) : base(message)
        {
        }

        public BaseHttpException(string type, T1 id) : base(GetMessage(type, id))
        {
        }
    }

    public class BaseHttpException<T1, T2> : BaseHttpException<T1>
    {
        public BaseHttpException(string type, string id1Message, T1 id1, string id2Message, T2 id2)
            : base(GetMessage(type, id1Message, id1, id2Message, id2))
        {
        }
    }
}
