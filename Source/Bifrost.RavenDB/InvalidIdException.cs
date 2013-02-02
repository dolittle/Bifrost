using System;
using System.Runtime.Serialization;


namespace Bifrost.RavenDB
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException()
        {
        }

        public InvalidIdException(string message)
            : base(message)
        {
        }

        public InvalidIdException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidIdException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}