/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
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
        /// <param name="identifier"><see cref="string"/> representing an identifier for the match</param>
        /// <param name="source"><see cref="ISegment"/> as source of the match</param>
        /// <param name="values"><see cref="IEnumerable{T}">strings</see> matched</param>
        /// <param name="children"><see cref="IEnumerable{T}">Children</see> that was mathched</param>
        public SegmentMatch(string identifier, ISegment source, IEnumerable<string> values, IEnumerable<ISegmentMatch> children)
        {
            Identifier = identifier;
            Source = source;
            Values = values.ToArray();
            Children = children;
        }

        /// <inheritdoc/>
        public string Identifier { get; }

        /// <inheritdoc/>
        public bool HasMatch { get { return Values.Count() > 0 || Children.Any(c => c.HasMatch); } }

        /// <inheritdoc/>
        public ISegment Source { get; }

        /// <inheritdoc/>
        public IEnumerable<string> Values { get; }

        /// <inheritdoc/>
        public IEnumerable<ISegmentMatch> Children { get; }
    }
}
