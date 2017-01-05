/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost
{
    /// <summary>
    /// The exception that is thrown when a type is missing a default constructor and one is required
    /// </summary>
    public class MissingDefaultConstructorException : ArgumentException
    {
        /// <summary>
        /// Initializes an instance of <see cref="MissingDefaultConstructorException"/>
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that is missing a constructor</param>
        public MissingDefaultConstructorException(Type type) : base(string.Format("Type '{0}' is missing a default constructor and one is required", type.FullName)) { }
    }
}
