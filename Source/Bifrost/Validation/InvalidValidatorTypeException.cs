using System;

namespace Bifrost.Validation
{
    /// <summary>
    /// Exception that is thrown if a validator type is of wrong type
    /// </summary>
    public class InvalidValidatorTypeException : ArgumentException
    {
        /// <summary>
        /// Initializes an instance of <see cref="InvalidValidatorTypeException"/>
        /// </summary>
        public InvalidValidatorTypeException()
        {
        }

        /// <summary>
        /// Initializes an instance of <see cref="InvalidValidatorTypeException"/> with a message
        /// </summary>
        public InvalidValidatorTypeException(string message)
            : base(message)
        {
        }
    }
}
