/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents a base class for <see cref="ISegment"/> 
    /// </summary>
    public abstract class Segment : ISegment
    {
        /// <inheritdoc/>
        public ISegment Parent { get; protected set; }

        /// <inheritdoc/>
        public IEnumerable<ISegment> Children { get; protected set; }

        /// <inheritdoc/>
        public SegmentOccurrence Occurrences { get; protected set; }

        /// <inheritdoc/>
        public bool Optional { get; protected set; }

        /// <inheritdoc/>
        public abstract bool Fixed { get; }

        /// <inheritdoc/>
        public abstract ISegmentMatch Match(IEnumerable<string> input);
    }
}
