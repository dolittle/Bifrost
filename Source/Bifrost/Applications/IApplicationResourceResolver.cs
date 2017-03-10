/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a system that can resolve <see cref="IApplicationResource">application resources</see>
    /// typically from <see cref="ApplicationResourceIdentifier"/> to a concrete <see cref="Type"/>
    /// </summary>
    public interface IApplicationResourceResolver
    {
        /// <summary>
        /// Resolve a <see cref="ApplicationResourceIdentifier"/> into a <see cref="Type"/>
        /// </summary>
        /// <param name="identifier"><see cref="ApplicationResourceIdentifier"/> to resolve</param>
        /// <returns>Resolved <see cref="Type"/></returns>
        Type Resolve(ApplicationResourceIdentifier identifier);
    }
}
