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
    /// Represents a <see cref="ISegment"/> for an arbitrary string
    /// </summary>
    public class FixedStringSegment : Segment
    {
        /// <summary>
        /// Initializes a new instance <see cref="FixedStringSegment"/>
        /// </summary>
        /// <param name="string">The <see cref="string"/> to expect</param>
        /// <param name="optional">Wether or not if the segment is optional</param>
        /// <param name="occurrences">Occurrences of the segment</param>
        /// <param name="parent">Parent <see cref="ISegment"/></param>
        /// <param name="children"><see cref="IEnumerable{ISegment}">Children</see></param>
        public FixedStringSegment(
            string @string, 
            bool optional, 
            SegmentOccurrence occurrences, 
            ISegment parent, 
            IEnumerable<ISegment> children)
        {
            String = @string;
            Optional = optional;
            Occurrences = occurrences;
            Parent = parent;
            Children = children;
        }

        /// <inheritdoc/>
        public override bool Fixed => true;

        /// <summary>
        /// Gets the expected <see cref="string"/>
        /// </summary>
        public string String { get; }

        /// <inheritdoc/>
        public override ISegmentMatch Match(IEnumerable<string> input)
        {
            var matches = new List<string>();
            var matchAndAdd = new Func<string, bool>((string s) =>
            {
                if (s == String)
                {
                    matches.Add(s);
                    return true;
                }
                else return false;
            });

            if (Occurrences == SegmentOccurrence.Single) matchAndAdd(input.First());
            else
            {
                var inputAsArray = input.ToArray();
                if (matchAndAdd(inputAsArray[0]) && inputAsArray.Length > 1)
                    for (var inputIndex = 1; inputIndex < inputAsArray.Length; inputIndex++)
                        if (!matchAndAdd(inputAsArray[inputIndex])) break;
            }

            IEnumerable<ISegmentMatch> matchesFromChildren = new ISegmentMatch[0];
            if( Optional == true || matches.Count > 0 )
                matchesFromChildren = MatchChildren(input, matches);

            var segmentMatch = new SegmentMatch(String, this, matches, matchesFromChildren);
            return segmentMatch;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"FixedStringSegment({String})";
        }

        IEnumerable<ISegmentMatch> MatchChildren(IEnumerable<string> input, IEnumerable<string> matches)
        {
            var matchesFromChildren = new List<ISegmentMatch>();
            var remainderToMatch = input.Skip(matches.Count());
            foreach (var child in Children)
            {
                if (remainderToMatch.Count() == 0) break;
                var match = child.Match(remainderToMatch);
                if (match.HasMatch)
                {
                    matchesFromChildren.Add(match);
                    remainderToMatch = remainderToMatch.Skip(match.Values.Count());
                }
            }

            return matchesFromChildren;
        }
    }
}
