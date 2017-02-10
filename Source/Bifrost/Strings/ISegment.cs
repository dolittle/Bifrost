/*---------------------------------------------------------------------------------------------
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
        SegmentOccurrence Occurrences { get; } 

        /// <summary>
        /// Gets wether or not the <see cref="ISegment">segment</see>
        /// </summary>
        bool Optional { get; }

        /// <summary>
        /// Gets any child <see cref="ISegment"/> that depends on this to exist
        /// </summary>
        IEnumerable<ISegment> Children { get; }

        /// <summary>
        /// Gets wether or not the segment is fixed and then acts as a divider
        /// </summary>
        /// <remarks>
        /// Fixed segments in a string represents a divider. Between dividers other
        /// types of segments, such as the <see cref="VariableStringSegment"/> can typically be 
        /// matched with a reccuring occurence. Without dividers, these strings would with reccurrence
        /// in some cases match all strings - as they are considered variables
        /// </remarks>
        bool Fixed { get; }

        /// <summary>
        /// Run matching against a set of strings
        /// </summary>
        /// <param name="input">An <see cref="IEnumerable{T}"/> to do a match for</param>
        /// <returns><see cref="ISegmentMatch"/> with any matches</returns>
        ISegmentMatch Match(IEnumerable<string> input);
    }
}
