/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.FluentValidation
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
