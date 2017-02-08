/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents a match for a <see cref="Segment"/>
    /// </summary>
    public class SegmentMatch : ISegmentMatch
    {
        /// <inheritdoc/>
        public bool HasMatch { get; }

        /// <inheritdoc/>
        public ISegment Source { get; }

        /// <inheritdoc/>
        public IEnumerable<string> Values { get; }
    }
}
