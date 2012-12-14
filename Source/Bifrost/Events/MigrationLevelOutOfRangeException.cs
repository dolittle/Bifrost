using System;
using System.Runtime.Serialization;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an exception situation where a <see cref="EventMigrationHierarchy">EventMigrationHierarchy</see> is
    /// asked for a concrete type at a level that does not exist.
    /// 
    /// This could be a level less than 0, or a level greater than the hierarchy depth.
    /// </summary>
    public class MigrationLevelOutOfRangeException : Exception
    {
        /// <summary>
        /// Initializes a <see cref="MigrationLevelOutOfRangeException">MigrationLevelOutOfRangeException</see>
        /// </summary>
        public MigrationLevelOutOfRangeException()
        {}

        /// <summary>
        /// Initializes a <see cref="MigrationLevelOutOfRangeException">MigrationLevelOutOfRangeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        public MigrationLevelOutOfRangeException(string message) : base(message)
        {}

        /// <summary>
        /// Initializes a <see cref="MigrationLevelOutOfRangeException">MigrationLevelOutOfRangeException</see>
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="innerException">Inner Exception</param>
        public MigrationLevelOutOfRangeException(string message, Exception innerException) : base(message,innerException)
        { }

#if(!SILVERLIGHT && !NETFX_CORE)
        /// <summary>
        /// Initializes a <see cref="MigrationLevelOutOfRangeException">MigrationLevelOutOfRangeException</see> for serialization
        /// </summary>
        /// <param name="serializationInfo">Serialization Info</param>
        /// <param name="streamingContext">Streaming Context</param>
        protected MigrationLevelOutOfRangeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo,streamingContext)
        {}
#endif
    }
}