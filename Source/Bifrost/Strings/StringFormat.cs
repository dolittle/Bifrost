/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

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
                .String(SegmentOccurence.Single)
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
            throw new NotImplementedException();
        }

        void ThrowIfMissingSeparators(char[] separators)
        {
            if (separators.Length == 0) throw new MissingSeparator();
        }
    }
}
