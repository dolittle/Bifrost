/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// Defines a string format for matching and extracting values from other strings 
    /// </summary>
    public interface IStringFormat
    {
        /// <summary>
        /// Gets the segments represented in the format
        /// </summary>
        IEnumerable<ISegment>   Segments { get; }

        /// <summary>
        /// Gets the string segment separators
        /// </summary>
        char[] Separators { get; }

        /// <summary>
        /// Matches a string against the format
        /// </summary>
        /// <param name="stringToMatch"></param>
        /// <returns><see cref="ISegmentMatches"/> containing matching information</returns>
        ISegmentMatches Match(string stringToMatch);
    }
}
