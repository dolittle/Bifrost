/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Bifrost.Conventions;

namespace Bifrost.Read
{
    /// <summary>
    /// Defines a repository for dealing with <see cref="IReadModel"/>s.
    /// </summary>
    /// <typeparam name="T">The type of the read model to provide.</typeparam>
    /// <remarks>
    /// Types inheriting from this interface will be automatically registered and invoked by <see cref="ReadModelOf{T}"/>
    /// when no specific <see cref="IReadModelOf{T}"/> is found.
    /// </remarks>
    public interface IReadModelRepositoryFor<T> : IConvention where T : IReadModel
    {
        /// <summary>
        /// Gets a queryable to use for querying
        /// </summary>
        IQueryable<T> Query { get; }

        /// <summary>
        /// Insert a newly created <see cref="IReadModel"/>
        /// </summary>
        /// <param name="readModel"><see cref="IReadModel"/> to insert</param>
        void Insert(T readModel);

        /// <summary>
        /// Update an existing <see cref="IReadModel"/>
        /// </summary>
        /// <param name="readModel"><see cref="IReadModel"/> to update</param>
        void Update(T readModel);

        /// <summary>
        /// Delete an existing <see cref="IReadModel"/>
        /// </summary>
        /// <param name="readModel"><see cref="IReadModel"/> to delete</param>
        void Delete(T readModel);

        /// <summary>
        /// Get a <see cref="IReadModel"/> by its id
        /// </summary>
        /// <param name="id">Id to lookup</param>
        /// <returns></returns>
        T GetById(object id);
    }
}
