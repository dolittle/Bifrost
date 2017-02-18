/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an identifier for <see cref="IApplicationResource">resources</see> in an <see cref="IApplication"/>
    /// </summary>
    public class ApplicationResourceIdentifier
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationResourceIdentifier"/>
        /// </summary>
        /// <param name="application"><see cref="IApplication"/> the resource belongs to</param>
        /// <param name="locationSegments"><see cref="IApplicationLocation">Location</see> segments for the <see cref="IApplicationResource"/></param>
        /// <param name="resource"><see cref="IApplicationResource">Resource</see> the identifier is for</param>
        public ApplicationResourceIdentifier(IApplication application, IEnumerable<IApplicationLocation> locationSegments, IApplicationResource resource)
        {
            Application = application;
            LocationSegments = locationSegments;
            Resource = resource;
        }

        /// <summary>
        /// Gets the <see cref="IApplication"/> the resource belongs to
        /// </summary>
        public IApplication Application { get; }

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