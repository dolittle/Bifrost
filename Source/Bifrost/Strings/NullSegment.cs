﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents a null <see cref="ISegment"/>
    /// </summary>
    public class NullSegment : ISegment
    {
        /// <inheritdoc/>
        public IEnumerable<ISegment> Children { get; }

        /// <inheritdoc/>
        public SegmentOccurence Occurrences { get; }

        /// <inheritdoc/>
        public bool Optional { get; }

        /// <inheritdoc/>
        public ISegment Parent { get; }
    }
}