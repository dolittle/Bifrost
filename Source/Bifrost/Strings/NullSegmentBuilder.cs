/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Strings
{
    /// <summary>
    /// Builds a <see cref="NullSegment"/>
    /// </summary>
    public class NullSegmentBuilder : ISegmentBuilder
    {
        /// <inheritdoc/>
        public bool DependsOnPrevious { get; }

        /// <inheritdoc/>
        public bool HasChildren => false;

        /// <inheritdoc/>
        public void AddChild(ISegmentBuilder child)
        {
            
        }

        /// <inheritdoc/>
        public ISegment Build()
        {
            return new NullSegment();
        }
    }
}
