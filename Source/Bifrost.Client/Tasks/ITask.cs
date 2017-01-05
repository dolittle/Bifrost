/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Defines a task that gets executed
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// The method that gets called for executing the actual task
        /// </summary>
        /// <param name="context">The <see cref="TaskContext"/> that the task is running in</param>
        /// <returns>A promise that is used to know when the task is done</returns>
        Promise Execute(TaskContext context);
    }
}
