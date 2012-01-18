using System;
using System.Runtime.Serialization;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an exceptional situation where an <see cref="IEvent">Event</see> in an <see cref="EventMigrationHierarchy">EventMigrationHierarchy</see>
    /// has not implemented the correct <see cref="Bifrost.Events.IAmNextGenerationOf{T}">interface</see>.
    /// </summary>
    public class NotAMigratedEventTypeException : Exception
    {
        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see>
        /// </summary>
        public NotAMigratedEventTypeException()
        {}

        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        public NotAMigratedEventTypeException(string message) : base(message)
        {}

        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="innerException">Inner Exception</param>
        public NotAMigratedEventTypeException(string message, Exception innerException) : base(message,innerException)
        {}

#if(!SILVERLIGHT)
        /// <summary>
        /// Initializes a <see cref="NotAMigratedEventTypeException">NotAMigratedEventTypeException</see> for serialization
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected NotAMigratedEventTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo,streamingContext)
        {}
#endif
    }
}