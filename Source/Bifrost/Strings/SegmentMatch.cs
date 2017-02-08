/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents a match for a <see cref="Segment"/>
    /// </summary>
    public class SegmentMatch : ISegmentMatch
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SegmentMatch"/>
        /// </summary>
        /// <param name="source"><see cref="ISegment"/> as source of the match</param>
        /// <param name="values"><see cref="IEnumerable{T}">strings</see> matched</param>
        public SegmentMatch(ISegment source, IEnumerable<string> values)
        {
            Source = source;
            Values = values.ToArray();
        }

        /// <inheritdoc/>
        public bool HasMatch { get { return Values.Count() > 0; } }

        /// <inheritdoc/>
        public ISegment Source { get; }

        /// <inheritdoc/>
        public IEnumerable<string> Values { get; }
    }
}
