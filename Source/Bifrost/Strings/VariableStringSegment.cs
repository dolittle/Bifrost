/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Logging;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents a <see cref="ISegment"/> for an arbitrary string
    /// </summary>
    public class VariableStringSegment : Segment
    {
        /// <summary>
        /// Initializes a new instance <see cref="FixedStringSegment"/>
        /// </summary>
        /// <param name="variableName">The variable name to expect</param>
        /// <param name="optional">Wether or not if the segment is optional</param>
        /// <param name="occurrences">Occurrences of the segment</param>
        /// <param name="parent">Parent <see cref="ISegment"/></param>
        /// <param name="children"><see cref="IEnumerable{ISegment}">Children</see></param>
        public VariableStringSegment(
            string variableName, 
            bool optional, 
            SegmentOccurrence occurrences, 
            ISegment parent, 
            IEnumerable<ISegment> children)
        {
            VariableName = variableName;
            Optional = optional;
            Occurrences = occurrences;
            Parent = parent;
            Children = children;
        }

        /// <inheritdoc/>
        public override bool Fixed => false;

        /// <summary>
        /// Gets the expected <see cref="string"/>
        /// </summary>
        public string VariableName { get; }

        /// <inheritdoc/>
        public override ISegmentMatch Match(IEnumerable<string> input)
        {
            var matches = new List<string>();

            Logger.Internal.Trace($"Matching : {string.Join(";",input)}");

            IEnumerable<ISegmentMatch> matchesFromChildren = new ISegmentMatch[0];
            if (Occurrences == SegmentOccurrence.Single)
            {
                matches.Add(input.First());
                matchesFromChildren = MatchChildren(input, matches);
            }
            else matches.AddRange(input);

            var match = new SegmentMatch(VariableName, this, matches, matchesFromChildren);
            return match;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"VariableStringSegment({VariableName})";
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
