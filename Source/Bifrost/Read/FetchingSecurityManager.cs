/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Security;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents an implementation of <see cref="IFetchingSecurityManager"/>
    /// </summary>
    public class FetchingSecurityManager : IFetchingSecurityManager
    {
        ISecurityManager _securityManager;

        /// <summary>
        /// Initializes a new instance of <see cref="FetchingSecurityManager"/>
        /// </summary>
        /// <param name="securityManager"><see cref="ISecurityManager"/> for forwarding requests related to security to</param>
        public FetchingSecurityManager(ISecurityManager securityManager)
        {
            _securityManager = securityManager;
        }

#pragma warning disable 1591 // Xml Comments
        public AuthorizationResult Authorize<T>(IReadModelOf<T> readModelOf) where T : IReadModel
        {
            return _securityManager.Authorize<Fetching>(readModelOf);
        }

        public AuthorizationResult Authorize(IQuery query)
        {
            return _securityManager.Authorize<Fetching>(query);
        }

        public AuthorizationResult Authorize<T>(IQueryFor<T> queryFor) where T : IReadModel
        {
            return _securityManager.Authorize<Fetching>(queryFor);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
