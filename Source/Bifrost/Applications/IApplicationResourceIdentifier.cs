/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Applications
{
    /// <summary>
    /// Defines an identifier for <see cref="IApplicationResource">resources</see> in an <see cref="IApplication"/>
    /// </summary>
    public interface IApplicationResourceIdentifier : IEquatable<IApplicationResourceIdentifier>, IComparable, IComparable<IApplicationResourceIdentifier>
    {
        /// <summary>
        /// Gets the <see cref="IApplication"/> the resource belongs to
        /// </summary>
        IApplication Application { get; }

        /// <summary>
        /// Gets the segments representing the full <see cref="IApplicationLocation">location</see>
        /// </summary>
        IEnumerable<IApplicationLocation> LocationSegments { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationResource">resource</see>
        /// </summary>
        IApplicationResource Resource { get; }
    }
}
