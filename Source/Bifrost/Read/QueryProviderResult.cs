/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents the result of issuing a query for a provider
    /// </summary>
    public class QueryProviderResult
    {
        /// <summary>
        /// Gets or sets the count of total items from a query
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the items as the result of a query
        /// </summary>
        public IEnumerable Items { get; set; }
    }
}
