/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Security.Principal;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a principal resolver that resolves from current thread;
    /// </summary>
    public class DefaultPrincipalResolver : ICanResolvePrincipal
    {
#pragma warning disable 1591 // Xml Comments
        public IPrincipal Resolve()
        {
            return GenericPrincipal.Current;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
