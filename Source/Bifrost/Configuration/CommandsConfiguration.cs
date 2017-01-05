/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Commands;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Represents an implementation of <see cref="ICommandsConfiguration"/>
	/// </summary>
    public class CommandsConfiguration : ConfigurationStorageElement, ICommandsConfiguration
	{
#pragma warning disable 1591 // Xml Comments
        public Type CommandCoordinatorType { get; set; }

        public override void Initialize(IContainer container)
        {
            if (CommandCoordinatorType != null)
                container.Bind<ICommandCoordinator>(CommandCoordinatorType);

            if (EntityContextConfiguration != null)
            {
                base.Initialize(container);
            }
        }
                
#pragma warning restore 1591 // Xml Comments
    }
}