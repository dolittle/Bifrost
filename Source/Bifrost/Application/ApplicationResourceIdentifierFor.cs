/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationResourceIdentifierFor<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationResourceIdentifierFor{T}"/>
        /// </summary>
        /// <param name="locationSegments"><see cref="IApplicationLocation">Location</see> segments for the <see cref="IApplicationResource"/></param>
        /// <param name="resource"><see cref="IApplicationResource">Resource</see> the identifier is for</param>
        public ApplicationResourceIdentifierFor(IEnumerable<IApplicationLocation> locationSegments, IApplicationResource resource)
        {
            LocationSegments = locationSegments;
            Resource = resource;
        }

        /// <summary>
        /// Gets the segments representing the full <see cref="IApplicationLocation">location</see>
        /// </summary>
        public IEnumerable<IApplicationLocation> LocationSegments { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationResource">resource</see>
        /// </summary>
        public IApplicationResource Resource { get; }
    }
}