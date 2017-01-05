/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;
using Bifrost.Tasks;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="ITasksConfiguration"/>
    /// </summary>
    public class TasksConfiguration : ConfigurationStorageElement, ITasksConfiguration
    {
#pragma warning disable 1591 // Xml Comments
        public override void Initialize(IContainer container)
        {
            if (EntityContextConfiguration != null)
                EntityContextConfiguration.BindEntityContextTo<TaskEntity>(container);

            base.Initialize(container);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
