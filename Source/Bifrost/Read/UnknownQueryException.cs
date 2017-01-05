/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Read
{
    /// <summary>
    /// The exception that is thrown when a query is not known by its name in the system
    /// </summary>
    public class UnknownQueryException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnknownQueryException"/>
        /// </summary>
        /// <param name="name"></param>
        public UnknownQueryException(string name)
            : base("There is no query named : " + name)
        {
        }
    }
}