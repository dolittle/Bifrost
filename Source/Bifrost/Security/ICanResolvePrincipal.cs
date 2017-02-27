/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Security.Principal;
using Bifrost.Conventions;

namespace Bifrost.Security
{
    /// <summary>
    /// Defines a resolver for <see cref="IPrincipal"/>.
    /// </summary>
    /// <remarks>
    /// An application may implement this convention once. If it is not implemented,
    /// the <see cref="DefaultPrincipalResolver"/> is used.
    /// </remarks>
    public interface ICanResolvePrincipal : IConvention
    {
        /// <summary>
        /// Method that is called to resolve current <see cref="IPrincipal"/>.
        /// </summary>
        /// <returns>The resolved <see cref="IPrincipal"/>.</returns>
        IPrincipal Resolve();
    }
}
