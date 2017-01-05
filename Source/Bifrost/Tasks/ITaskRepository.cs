/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Defines a repository for working with <see cref="Task">tasks</see>
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Load all tasks
        /// </summary>
        /// <returns>A collection of <see cref="Task">Tasks</see></returns>
        IEnumerable<Task> LoadAll();

        /// <summary>
        /// Load a specific <see cref="Task"/>
        /// </summary>
        /// <param name="taskId"><see cref="TaskId"/> of the task</param>
        /// <returns>The loaded <see cref="Task"/></returns>
        Task Load(TaskId taskId);

        /// <summary>
        /// Save a <see cref="Task"/>
        /// </summary>
        /// <param name="task"><see cref="Task"/> to save</param>
        void Save(Task task);

        /// <summary>
        /// Delete a task <see cref="Task"/>
        /// </summary>
        /// <param name="task"><see cref="Task"/> to delete</param>
        void Delete(Task task);

        /// <summary>
        /// Delete a <see cref="Task"/> by its <see cref="TaskId">identifier</see>
        /// </summary>
        /// <param name="taskId"><see cref="TaskId"/> of the task</param>
        void DeleteById(TaskId taskId);
    }
}
