/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents a null <see cref="ISegment"/>
    /// </summary>
    public class NullSegment : ISegment
    {
        /// <inheritdoc/>
        public IEnumerable<ISegment> Children { get; }

        /// <inheritdoc/>
        public bool Fixed => true;

        /// <inheritdoc/>
        public SegmentOccurrence Occurrences { get; }

        /// <inheritdoc/>
        public bool Optional { get; }

        /// <inheritdoc/>
        public ISegment Parent { get; }

        /// <inheritdoc/>
        public ISegmentMatch Match(IEnumerable<string> input)
        {
            return new SegmentMatch("Null", this, new string[0], new ISegmentMatch[0]);
        }


        /// <inheritdoc/>
        public override string ToString()
        {
            return "[NULL SEGMENT]";
        }

    }
}
