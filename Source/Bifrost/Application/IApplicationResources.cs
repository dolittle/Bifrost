/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Application
{
    /// <summary>
    /// Defines a utility for working with application resources
    /// </summary>
    public interface IApplicationResources
    {
        /// <summary>
        /// Identify a resource
        /// </summary>
        /// <typeparam name="T">Any type that there might be a resource map for</typeparam>
        /// <param name="resource">Resource to identify</param>
        /// <returns><see cref="ApplicationResourceIdentifierFor{T}"/> identifying the reosource</returns>
        ApplicationResourceIdentifierFor<T> Identify<T>(T resource);
    }
}
