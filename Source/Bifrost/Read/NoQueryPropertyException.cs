/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Read
{
    /// <summary>
    /// The exception that is thrown when a well known query does not have the query property on it
    /// </summary>
    public class NoQueryPropertyException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NoQueryPropertyException"/>
        /// </summary>
        /// <param name="query"><see cref="IQuery"/> that does not have the property on it</param>
        public NoQueryPropertyException(IQuery query)
            : base(string.Format("No query property for {0}. Hint: It should be a public instance property with a get on it.", query.GetType().FullName))
        {
        }
    }
}
