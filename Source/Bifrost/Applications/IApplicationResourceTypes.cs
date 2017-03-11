/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Defines a system that knows about all <see cref="IApplicationResourceTypes"/>
    /// </summary>
    public interface IApplicationResourceTypes
    {
        /// <summary>
        /// Get a <see cref="IApplicationResourceType"/> from a <see cref="string">identifier</see>
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get for</param>
        /// <returns><see cref="IApplicationResourceType"/></returns>
        IApplicationResourceType GetFor(Type type);

        /// <summary>
        /// Get a <see cref="IApplicationResourceType"/> from a <see cref="string">identifier</see>
        /// </summary>
        /// <param name="identifier"><see cref="string">Identifier</see> to get for</param>
        /// <returns><see cref="IApplicationResourceType"/></returns>
        IApplicationResourceType GetFor(string identifier);
    }
}
