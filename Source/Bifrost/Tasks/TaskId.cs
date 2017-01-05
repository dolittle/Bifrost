/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Defines an identifier for <see cref="Task">Tasks</see>
    /// </summary>
    public struct TaskId
    {
        /// <summary>
        /// Create a new <see cref="TaskId"/>
        /// </summary>
        /// <returns></returns>
        public static TaskId New()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the actual value
        /// </summary>
        public Guid Value { get; set; }

        /// <summary>
        /// Implicitly convert from <see cref="TaskId"/> to <see cref="Guid"/>
        /// </summary>
        /// <param name="taskId"><see cref="TaskId"/> to convert from</param>
        /// <returns>The <see cref="Guid"/> from the <see cref="TaskId"/></returns>
        public static implicit operator Guid(TaskId taskId)
        {
            return taskId.Value;
        }


        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="TaskId"/>
        /// </summary>
        /// <param name="taskId"><see cref="Guid"/> to convert from</param>
        /// <returns>The <see cref="TaskId"/> created from the <see cref="Guid"/> </returns>
        public static implicit operator TaskId(Guid taskId)
        {
            return new TaskId { Value = taskId };
        }
    }
}
