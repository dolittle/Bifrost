/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Defines an interface that is used to create a container instance
    /// </summary>
    public interface ICanCreateContainer
    {
        /// <summary>
        /// Creates an instance of the container that will be used throughout the application
        /// </summary>
        /// <returns>An instance of a <see cref="IContainer"/> implementation</returns>
        IContainer CreateContainer();
    }
}
