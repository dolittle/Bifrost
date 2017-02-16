/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Strings
{
    /// <summary>
    /// Extensions for <see cref="ISegmentMatches"/>
    /// </summary>
    public static class SegmentMatchesExtensions
    {
        /// <summary>
        /// Convert into a <see cref="IDictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="segmentMatches"></param>
        /// <returns></returns>
        public static IDictionary<string,IEnumerable<string>> AsDictionary(this ISegmentMatches segmentMatches)
        {
            var dictionary = segmentMatches.ToDictionary(s => s.Identifier, s => s.Values);
            return dictionary;
        }
    }
}
