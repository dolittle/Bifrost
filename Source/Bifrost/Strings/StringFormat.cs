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
    /// Represents an implementation of <see cref="IStringFormat"/>
    /// </summary>
    public class StringFormat : IStringFormat
    {
        /// <summary>
        /// Initializes an instance of <see cref="StringFormat"/>
        /// </summary>
        /// <param name="segments"><see cref="IEnumerable{T}">Segments</see> for the <see cref="StringFormat"/></param>
        /// <param name="separators">Separator characters for segments - must be at least one</param>
        public StringFormat(IEnumerable<ISegment> segments, params char[] separators)
        {
            ThrowIfMissingSeparators(separators);

            Segments = segments;
            Separators = separators;


            /*
            format
                .String(s => s.Single())
                .BoundedContext()
                .Module()
                .Feature()
                .SubFeature(f => f
                    .Recurring()
                    .Optional()
                    .DependingOnPrevious()
                )
            */
        }

        /// <inheritdoc/>
        public IEnumerable<ISegment> Segments { get; }

        /// <inheritdoc/>
        public char[] Separators { get; }

        /// <inheritdoc/>
        public ISegmentMatches Match(string stringToMatch)
        {
            var matches = new List<ISegmentMatch>();
            var strings = stringToMatch.Split(Separators);

            var currentIndex = 0;
            foreach( var segment in Segments )
            {
                var length = strings.Length - currentIndex;
                var stringSegments = new string[length];
                Array.Copy(strings, currentIndex, stringSegments, 0, length);
                var match = segment.Match(stringSegments);
                if (match.HasMatch)
                {
                    matches.Add(match);
                    currentIndex += match.Values.Count();
                }
            }

            var segmentMatches = new SegmentMatches(matches);
            return segmentMatches;
        }

        void ThrowIfMissingSeparators(char[] separators)
        {
            if (separators.Length == 0) throw new MissingSeparator();
        }
    }
}
