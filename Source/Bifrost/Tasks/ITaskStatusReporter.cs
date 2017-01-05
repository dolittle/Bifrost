/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Tasks
{
    /// <summary>
    /// Defines a interface for receiving status reports for tasks
    /// </summary>
    public interface ITaskStatusReporter
    {
        /// <summary>
        /// Gets called when a task has been started
        /// </summary>
        /// <param name="task"><see cref="Task"/> that was started</param>
        void Started(Task task);

        /// <summary>
        /// Gets called when a task has been stopped
        /// </summary>
        /// <param name="task"><see cref="Task"/> that was stopped</param>
        void Stopped(Task task);

        /// <summary>
        /// Gets called when a task has been paused
        /// </summary>
        /// <param name="task"></param>
        void Paused(Task task);

        /// <summary>
        /// Gets called when a task has been resumed
        /// </summary>
        /// <param name="task"><see cref="Task"/> that was resumed</param>
        void Resumed(Task task);

        /// <summary>
        /// Gets called when a task changes state
        /// </summary>
        /// <param name="task"><see cref="Task"/> that was changed</param>
        void StateChanged(Task task);
    }
}
