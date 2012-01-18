using System;
using System.Runtime.Serialization;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an exceptional situation where an <see cref="IEvent">Event</see> in an <see cref="EventMigrationHierarchy">EventMigrationHierarchy</see>
    /// has does not migrate from the previous event in the migration hierarchy.
    /// </summary>
    public class InvalidMigrationTypeException : Exception
    {
        /// <summary>
        /// Initializes a <see cref="InvalidMigrationTypeException">InvalidMigrationTypeException</see>
        /// </summary>
        public InvalidMigrationTypeException()
        {}

        /// <summary>
        /// Initializes a <see cref="InvalidMigrationTypeException">InvalidMigrationTypeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        public InvalidMigrationTypeException(string message) : base(message)
        {}

        /// <summary>
        /// Initializes a <see cref="InvalidMigrationTypeException">InvalidMigrationTypeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="innerException">Inner Exception</param>
        public InvalidMigrationTypeException(string message, Exception innerException) : base(message,innerException)
        {}

#if(!SILVERLIGHT)
        /// <summary>
        /// Initializes a <see cref="InvalidMigrationTypeException">InvalidMigrationTypeException</see> for serialization
        /// </summary>
        /// <param name="serializationInfo">Serialization Info</param>
        /// <param name="streamingContext">Streaming Context</param>
        protected InvalidMigrationTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo,streamingContext)
        {}
#endif
    }
}