#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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
