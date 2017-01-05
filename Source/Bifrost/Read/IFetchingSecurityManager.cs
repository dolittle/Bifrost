/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Security;

namespace Bifrost.Read
{
    /// <summary>
    /// Defines a manager for dealing with security for <see cref="Fetching">fetching read models</see>
    /// </summary>
    public interface IFetchingSecurityManager
    {
        /// <summary>
        /// Authorizes a <see cref="IReadModelOf{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IReadModel"/> - typically inferred by usage</typeparam>
        /// <param name="readModelOf"><see cref="IReadModelOf{T}"/> to authorize</param>
        /// <returns><see cref="AuthorizationResult"/> that details how the <see cref="IReadModelOf{T}"/> was authorized</returns>
        AuthorizationResult Authorize<T>(IReadModelOf<T> readModelOf) where T : IReadModel;

        /// <summary>
        /// Authorizes a <see cref="IQuery"/>
        /// </summary>
        /// <param name="query"><see cref="IQuery"/> to authorize</param>
        /// <returns><see cref="AuthorizationResult"/> that details how the <see cref="IQueryFor{T}"/> was authorized</returns>
        AuthorizationResult Authorize(IQuery query);

        /// <summary>
        /// Authorizes a <see cref="IQueryFor{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IReadModel"/> - typically inferred by usage</typeparam>
        /// <param name="queryFor"><see cref="IQueryFor{T}"/> to authorize</param>
        /// <returns><see cref="AuthorizationResult"/> that details how the <see cref="IQueryFor{T}"/> was authorized</returns>
        AuthorizationResult Authorize<T>(IQueryFor<T> queryFor) where T : IReadModel;
    }
}
