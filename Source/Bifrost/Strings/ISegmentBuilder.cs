/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Strings
{
    /// <summary>
    /// Represents a builder for <see cref="ISegment"/>
    /// </summary>
    public interface ISegmentBuilder
    {
        /// <summary>
        /// Gets wether or not there are children
        /// </summary>
        bool HasChildren { get; }

        /// <summary>
        /// Gets wether or not this segment depends on previous
        /// </summary>
        bool DependsOnPrevious { get; }

        /// <summary>
        /// Add a <see cref="ISegmentBuilder"/> as a child
        /// </summary>
        /// <param name="child"></param>
        void AddChild(ISegmentBuilder child);

        /// <summary>
        /// Build the <see cref="ISegment"/>
        /// </summary>
        /// <returns>A <see cref="ISegment"/> instance</returns>
        ISegment Build();
    }

    /// <summary>
    /// Represents a typed builder for <see cref="ISegment"/>
    /// </summary>
    public interface ISegmentBuilder<T> : ISegmentBuilder
        where T:ISegment
    {
        /// <summary>
        /// Build the <see cref="ISegment"/>
        /// </summary>
        /// <returns>A <see cref="ISegment"/> instance</returns>
        new T Build();
    }
}
