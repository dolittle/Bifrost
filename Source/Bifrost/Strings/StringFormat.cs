/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// 
    /// </summary>
    public class StringFormat : IStringFormat
    {
        /// <summary>
        /// Initializes an instance of <see cref="StringFormat"/>
        /// </summary>
        /// <param name="segments"></param>
        public StringFormat(IEnumerable<ISegment> segments)
        {
            Segments = segments;

            /*
            format
                .String(SegmentOccurence.Single)
                .BoundedContext()
                .Module()
                .Feature()
                .SubFeature(f => f
                    .Recurse()
                    .Optional()
                    .DependingOnPrevious()
                )
            */
        }

        /// <inheritdoc/>
        public IEnumerable<ISegment> Segments { get; }
    }
}
