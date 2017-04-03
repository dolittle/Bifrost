/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Strings
{
    /// <summary>
    /// Builder for <see cref="FixedStringSegment"/>
    /// </summary>
    public interface IFixedStringSegmentBuilder : ISegmentBuilder<FixedStringSegment>
    {
        /// <summary>
        /// Expect a single instance of <see cref="FixedStringSegment"/> 
        /// </summary>
        /// <returns><see cref="IFixedStringSegmentBuilder"/> to continue to build on</returns>
        IFixedStringSegmentBuilder Single();

        /// <summary>
        /// Expect to find multiple instances of this <see cref="FixedStringSegment"/>
        /// </summary>
        /// <returns><see cref="IFixedStringSegmentBuilder"/> to continue to build on</returns>
        IFixedStringSegmentBuilder Recurring();

        /// <summary>
        /// Mark <see cref="FixedStringSegment"/> as optional
        /// </summary>
        /// <returns><see cref="IFixedStringSegmentBuilder"/> to continue to build on</returns>
        IFixedStringSegmentBuilder Optional();

        /// <summary>
        /// Make the <see cref="FixedStringSegment"/> a child
        /// </summary>
        /// <returns><see cref="IFixedStringSegmentBuilder"/> to continue to build on</returns>
        IFixedStringSegmentBuilder DependingOnPrevious();
    }
}