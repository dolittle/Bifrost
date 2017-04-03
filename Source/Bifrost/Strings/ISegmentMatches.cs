/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// Defines all the <see cref="ISegmentMatch">matches</see> from matching a full string with
    /// a <see cref="IStringFormat"/> 
    /// </summary>
    public interface ISegmentMatches : IEnumerable<ISegmentMatch>
    {
        /// <summary>
        /// Gets wether or not it found any matches
        /// </summary>
        bool HasMatches { get; }
    }
}