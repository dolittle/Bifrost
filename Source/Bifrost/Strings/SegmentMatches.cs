/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents an implementation of <see cref="ISegmentMatches"/>
    /// </summary>
    public class SegmentMatches : ISegmentMatches
    {
        IEnumerable<ISegmentMatch> _segmentMatches;

        /// <summary>
        /// Initializes a new instance of <see cref="SegmentMatches"/>
        /// </summary>
        /// <param name="segmentMatches"><see cref="IEnumerable{ISegmentMatch}">Segment matches</see></param>
        public SegmentMatches(IEnumerable<ISegmentMatch> segmentMatches)
        {
            _segmentMatches = segmentMatches;
        }

        /// <inheritdoc/>
        public bool HasMatches => _segmentMatches.Count() > 0;

        /// <inheritdoc/>
        public IEnumerator<ISegmentMatch> GetEnumerator()
        {
            return _segmentMatches.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _segmentMatches.GetEnumerator();
        }
    }
}
