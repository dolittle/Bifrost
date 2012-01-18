using System;
using System.Runtime.Serialization;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an exceptional situation where an event in an <see cref="EventMigrationHierarchy">EventMigrationHierarchy</see>
    /// has more than one migration path.
    /// </summary>
    public class DuplicateInEventMigrationHierarchyException : Exception
    {
		/// <summary>
		/// Initializes an instance of <see cref="DuplicateInEventMigrationHierarchyException"/>
		/// </summary>
        public DuplicateInEventMigrationHierarchyException()
        {}

		/// <summary>
		/// Initializes an instance of <see cref="DuplicateInEventMigrationHierarchyException"/>
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		public DuplicateInEventMigrationHierarchyException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes an instance of <see cref="DuplicateInEventMigrationHierarchyException"/>
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		/// <param name="innerException">The inner exception that is causing the exception</param>
		public DuplicateInEventMigrationHierarchyException(string message, Exception innerException)
			: base(message, innerException)
        {}

#if(!SILVERLIGHT)
        /// <summary>
        /// Initializes an instance of <see cref="DuplicateInEventMigrationHierarchyException"/> for serialization
        /// </summary>
        /// <param name="serializationInfo">Serialization Info</param>
        /// <param name="streamingContext">Streaming Context</param>
        protected DuplicateInEventMigrationHierarchyException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo,streamingContext)
        {}
#endif
    }
}