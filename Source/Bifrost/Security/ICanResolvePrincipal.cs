/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Security.Principal;

namespace Bifrost.Security
{
    /// <summary>
    /// Defines a resolver for <see cref="IIdentity"/>
    /// </summary>
    public interface ICanResolvePrincipal
    {
        /// <summary>
        /// Resolve current <see cref="IPrincipal"/>
        /// </summary>
        /// <returns>The resolved <see cref="IPrincipal"/></returns>
        IPrincipal Resolve();
    }
}
