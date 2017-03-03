/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Conventions;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Defines an interface that is used to create a container instance.
    /// </summary>
    /// <remarks>
    /// An application must implement this convention exactly once.
    /// Implementations of this type must have a default constructor.
    /// </remarks>
    public interface ICanCreateContainer : IConvention
    {
        /// <summary>
        /// Creates an instance of the container that will be used throughout the application.
        /// </summary>
        /// <returns>An instance of a <see cref="IContainer"/> implementation.</returns>
        IContainer CreateContainer();
    }
}
