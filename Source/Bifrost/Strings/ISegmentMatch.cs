/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// Defines a <see cref="ISegment"/> match
    /// </summary>
    public interface ISegmentMatch
    {
        /// <summary>
        /// Gets the string identifying the <see cref="ISegmentMatch"/>
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Gets wether or not there was a match
        /// </summary>
        bool HasMatch { get; }

        /// <summary>
        /// Gets the <see cref="ISegment">source</see>
        /// </summary>
        ISegment Source { get; }

        /// <summary>
        /// Gets the values associated with the match
        /// </summary>
        IEnumerable<string> Values { get; }

        /// <summary>
        /// Gets any child matches
        /// </summary>
        IEnumerable<ISegmentMatch> Children { get; }
    }
}