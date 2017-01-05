/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Mapping
{
    /// <summary>
    /// Defines a mapper that is capable of mapping objects
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Check if it is possible to map two types
        /// </summary>
        /// <typeparam name="TTarget">Target type to map to</typeparam>
        /// <typeparam name="TSource">Type of the source we're mapping from</typeparam>
        /// <returns></returns>
        bool CanMap<TTarget, TSource>();

        /// <summary>
        /// Map an existing object and create a new while doing so
        /// </summary>
        /// <typeparam name="TTarget">Target type to create</typeparam>
        /// <typeparam name="TSource">Type of the source we're mapping from</typeparam>
        /// <param name="source">Source object</param>
        /// <returns>A new mapped instance</returns>
        TTarget Map<TTarget, TSource>(TSource source);

        /// <summary>
        /// Map to an existing instance of an object
        /// </summary>
        /// <typeparam name="TTarget">Type of the target we're mapping to</typeparam>
        /// <typeparam name="TSource">Type of the source we're mapping from</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="target">Target object</param>
        void MapToInstance<TTarget, TSource>(TSource source, TTarget target);

    }
}
