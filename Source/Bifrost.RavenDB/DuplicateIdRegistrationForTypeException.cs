using System;
using System.Runtime.Serialization;

namespace Bifrost.RavenDB
{
    public class DuplicateIdRegistrationForTypeException : Exception
    {
        public DuplicateIdRegistrationForTypeException()
        {
        }

        public DuplicateIdRegistrationForTypeException(string message)
            : base(message)
        {
        }

        public DuplicateIdRegistrationForTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DuplicateIdRegistrationForTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}