/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Logging;

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
        }

        /// <summary>
        /// Build a <see cref="IStringFormat"/> 
        /// </summary>
        /// <param name="builderCallback"></param>
        /// <param name="separators">Separators that separates strings - at least one</param>
        /// <returns><see cref="IStringFormat"/> representing the format</returns>
        public static IStringFormat Build(Action<IStringFormatBuilder> builderCallback, params char[] separators)
        {
            var builder = new StringFormatBuilder(separators);
            builderCallback(builder);
            return builder.Build();
        }

        /// <summary>
        /// Build a <see cref="IStringFormat"/> from a string
        /// </summary>
        /// <param name="format">Format of the string</param>
        /// <returns><see cref="IStringFormat"/> representing the format</returns>
        /// <remarks>
        /// See <see cref="IStringFormatParser"/> for more details on format
        /// </remarks>
        public static IStringFormat Parse(string format)
        {
            var parser = new StringFormatParser();
            var stringFormat = parser.Parse(format);
            return stringFormat;
        }

        /// <inheritdoc/>
        public IEnumerable<ISegment> Segments { get; }

        /// <inheritdoc/>
        public char[] Separators { get; }

        /// <inheritdoc/>
        public ISegmentMatches Match(string stringToMatch)
        {
            Logger.Internal.Trace($"Trying to match the following string '{stringToMatch}' - separators : '{string.Join(" ",Separators)}'");

            var matches = new List<ISegmentMatch>();
            var strings = stringToMatch.Split(Separators);

            var segments = Segments.ToArray();
            var currentStringIndex = 0;
            var currentSegmentIndex = 0;

            Logger.Internal.Trace($"Segments count: {segments.Length}");

            while( currentSegmentIndex < segments.Length )
            {
                var segment = segments[currentSegmentIndex];
                Logger.Internal.Trace($"Current segment : {segment}");

                var length = strings.Length - currentStringIndex;
                if (length <= 0) break;

                if (!segment.Fixed && currentSegmentIndex < segments.Length - 1)
                    length = FindNextStop(strings, segments, currentStringIndex, currentSegmentIndex, segment, length);

                if (length <= 0) break;

                var match = MatchStrings(strings, currentStringIndex, segment, length);
                if (!match.HasMatch && !segment.Optional) return new SegmentMatches(new ISegmentMatch[0]);
                if (match.HasMatch)
                {
                    Logger.Internal.Trace($"Match found");
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
                    var actualLength = length - 1;
                    if (actualLength <= 0) break;

                    var nextMatch = MatchStrings(strings, currentStringIndex + 1, segment, actualLength);
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
            if (separators.Length == 0) throw new MissingSeparators();
        }
    }
}
