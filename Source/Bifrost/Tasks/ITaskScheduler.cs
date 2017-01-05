/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Defines an executor for executing <see cref="Task">tasks</see>
    /// </summary>
    public interface ITaskScheduler
    {
        /// <summary>
        /// Start a <see cref="Task"/> and its <see cref="TaskOperation">operations</see>
        /// </summary>
        /// <param name="task"><see cref="Task"/> to execute</param>
        /// <param name="taskDone">Optional <see cref="Action{Task}"/> that gets called when the task is done</param>
        void Start(Task task, Action<Task> taskDone=null);

        /// <summary>
        /// Stops a <see cref="Task"/> that is executing
        /// </summary>
        /// <param name="task"><see cref="Task"/> to stop</param>
        void Stop(Task task);
    }
}
