/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Defines configuration for commands
	/// </summary>
    public interface ICommandsConfiguration : IConfigurationElement, IHaveStorage
    {
        /// <summary>
        /// Gets or sets the type <see cref="ICommandCoordinator"/> to use
        /// </summary>
        Type CommandCoordinatorType { get; set; }
    }
}