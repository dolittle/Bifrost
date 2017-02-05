﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// Defines a segment of a <see cref="IStringFormat"/>
    /// </summary>
    public interface ISegment
    {
        /// <summary>
        /// Gets the parent <see cref="ISegment"/>
        /// </summary>
        ISegment Parent { get; }

        /// <summary>
        /// Gets the number of instances for the segment
        /// </summary>
        SegmentOccurence Occurrences { get; } 

        /// <summary>
        /// Gets wether or not the <see cref="ISegment">segment</see>
        /// </summary>
        bool Optional { get; }

        /// <summary>
        /// Gets any child <see cref="ISegment"/> that depends on this to exist
        /// </summary>
        IEnumerable<ISegment> Children { get; }
    }
}