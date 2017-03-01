using System;

namespace Bifrost.Execution
{
    /// <summary>
    /// The exception that is thrown when specification of assemblies fails.
    /// </summary>
    public class AssemblySpecificationException : Exception
    {
        /// <summary>
        /// Initializes an instance of <see cref="AssemblySpecificationException"/>.
        /// </summary>
        public AssemblySpecificationException()
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="AssemblySpecificationException"/>.
        /// </summary>
        /// <param name="message">Message with details about the exception</param>.
        public AssemblySpecificationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="AssemblySpecificationException"/>.
        /// </summary>
        /// <param name="message">Message with details about the exception.</param>
        /// <param name="innerException">The exception that caused this exception.</param>
        public AssemblySpecificationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
