/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a resolver that can resolve <see cref="Type">types</see> from <see cref="IApplicationResourceIdentifier"/>
    /// </summary>
    public interface ICanResolveApplicationResources
    {
        /// <summary>
        /// Gets the supported <see cref="IApplicationResourceType"/>
        /// </summary>
        IApplicationResourceType ApplicationResourceType { get; }

        /// <summary>
        /// Resolve a <see cref="IApplicationResourceIdentifier"/> into a <see cref="Type"/>
        /// </summary>
        /// <param name="identifier"><see cref="IApplicationResourceIdentifier"/> to resolve</param>
        /// <returns>Resolved <see cref="Type"/></returns>
        Type Resolve(IApplicationResourceIdentifier identifier);
    }
}