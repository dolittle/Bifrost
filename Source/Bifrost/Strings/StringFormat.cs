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

            var segments = Segments.ToArray();
            var currentStringIndex = 0;
            var currentSegmentIndex = 0;

            while( currentSegmentIndex < segments.Length )
            {
                var segment = segments[currentSegmentIndex];
                var length = strings.Length - currentStringIndex;
                if (length <= 0) break;

                if (!segment.Fixed && currentSegmentIndex < segments.Length - 1)
                    length = FindNextStop(strings, segments, currentStringIndex, currentSegmentIndex, segment, length);

                if (length <= 0) break;

                var match = MatchStrings(strings, currentStringIndex, segment, length);
                if (match.HasMatch)
                {
                    matches.Add(match);
                    matches.AddRange(match.Children);
                    currentStringIndex += match.Values.Count();
                }
                currentSegmentIndex++;
            }

            var segmentMatches = new SegmentMatches(matches);
            return segmentMatches;
        }

        int FindNextStop(string[] strings, ISegment[] segments, int currentStringIndex, int currentSegmentIndex, ISegment segment, int length)
        {
            var nextSegment = segments[currentSegmentIndex + 1];
            if (nextSegment.Fixed)
            {
                for (var i = currentStringIndex; i < strings.Length; i++)
                {
                    var nextMatch = MatchStrings(strings, currentStringIndex + 1, segment, length - 1);
                    if (nextMatch.HasMatch)
                        length = strings.Length - (currentStringIndex + 1);

                }
            }

            return length;
        }

        ISegmentMatch MatchStrings(string[] strings, int currentStringIndex, ISegment segment, int length)
        {
            var stringSegmentsToMatch = new string[length];
            Array.Copy(strings, currentStringIndex, stringSegmentsToMatch, 0, length);
            var match = segment.Match(stringSegmentsToMatch);
            return match;
        }

        void ThrowIfMissingSeparators(char[] separators)
        {
            if (separators.Length == 0) throw new MissingSeparator();
        }
    }
}
