/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Read
{
    /// <summary>
    /// Defines the filtering system for <see cref="IReadModel">ReadModels</see>
    /// </summary>
    public interface IReadModelFilters
    {
        /// <summary>
        /// Filters an incoming <see cref="IEnumerable{IReadModel}"/>
        /// </summary>
        /// <param name="readModels"><see cref="IEnumerable{IReadModel}">ReadModels</see> to filter</param>
        /// <returns>Filtered <see cref="IEnumerable{IReadModel}">ReadModels</see></returns>
        IEnumerable<IReadModel> Filter(IEnumerable<IReadModel> readModels);
    }
}
