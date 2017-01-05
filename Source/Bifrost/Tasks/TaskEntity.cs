/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents an enity of a <see cref="Task"/> that can be persisted
    /// </summary>
    public class TaskEntity
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TaskEntity"/>
        /// </summary>
        public TaskEntity()
        {
            State = new Dictionary<string, string>();
        }


        /// <summary>
        /// Gets or sets the Id of the <see cref="Task"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the <see cref="Task"/>
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the current operation within the <see cref="Task"/>
        /// </summary>
        public int CurrentOperation { get; set; }

        /// <summary>
        /// Gets or sets any state that exists explicitly on the custom <see cref="Task"/>
        /// </summary>
        public IDictionary<string, string> State { get; private set; }
    }
}
