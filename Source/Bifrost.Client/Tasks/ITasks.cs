/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Defines a system that coordinates <see cref="ITask">tasks</see>
    /// </summary>
    public interface ITasks
    {
        /// <summary>
        /// Gets wether or not the task system is running tasks - indicating its busy
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Gets all tasks running in this system
        /// </summary>
        IEnumerable<ITask> All { get; }

        /// <summary>
        /// Gets all the contexts for running tasks in this system
        /// </summary>
        IEnumerable<TaskContext> Contexts { get; }

        /// <summary>
        /// Execute a task
        /// </summary>
        /// <param name="task">Task to execute</param>
        /// <returns><see cref="TaskContext"/> for the task</returns>
        TaskContext Execute(ITask task, object associatedData = null);
    }
}
