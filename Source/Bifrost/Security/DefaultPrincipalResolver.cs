/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Security.Claims;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a principal resolver that resolves from current thread;
    /// </summary>
    public class DefaultPrincipalResolver : ICanResolvePrincipal
    {
        /// <inheritdoc/>
        public ClaimsPrincipal Resolve()
        {
            return ClaimsPrincipal.Current;
        }
    }
}
