/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Conventions;

namespace Bifrost.Read
{
    /// <summary>
    /// Defines a filter that can be applied to a set of <see cref="IReadModel">ReadModels</see>.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface will be automatically registered.
    /// An application can implement any number of these conventions.
    /// Typically this is applied both when getting a single <see cref="IReadModel"/>
    /// and when executing a <see cref="IQueryFor{T}">Query</see> for a <see cref="IReadModel"/>.
    /// </remarks>
    public interface ICanFilterReadModels : IConvention
    {
        /// <summary>
        /// Filters an incoming <see cref="IEnumerable{IReadModel}"/>
        /// </summary>
        /// <param name="readModels"><see cref="IEnumerable{IReadModel}">ReadModels</see> to filter</param>
        /// <returns>Filtered <see cref="IEnumerable{IReadModel}">ReadModels</see></returns>
        IEnumerable<IReadModel> Filter(IEnumerable<IReadModel> readModels);
    }
}
