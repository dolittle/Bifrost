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
    public class UnknownQueryTypeException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnknownQueryTypeException"/>
        /// </summary>
        /// <param name="query"><see cref="IQuery"/> that does not have the property on it</param>
        /// <param name="type"><see cref="Type"/> of the expected query returned from the Query property</param>
        public UnknownQueryTypeException(IQuery query, Type type)
            : base(string.Format("Unable to find a query provider of type '{0}' for the query '{1}'. Hint: Are you sure the query return type has a known query provider for it?", type.FullName, query.GetType().FullName))
        {
        }
    }
}
