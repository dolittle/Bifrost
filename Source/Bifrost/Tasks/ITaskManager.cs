#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Bifrost.Tasks
{
    /// <summary>
    /// Defines a manager for working with <see cref="Task">Tasks</see>
    /// </summary>
    public interface ITaskManager
    {
        /// <summary>
        /// Resume a task with given <see cref="TaskId"/>
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Task"/> to resume</typeparam>
        /// <param name="taskId">The <see cref="TaskId"/> that identifies the task</param>
        /// <returns>The <see cref="Task"/>that is resumed</returns>
        T Resume<T>(TaskId taskId) where T : Task;

        /// <summary>
        /// Start a given task
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Task"/> to start</typeparam>
        /// <returns>The <see cref="Task"/> that was started</returns>
        T Start<T>() where T : Task;

        /// <summary>
        /// Stop a task by its <see cref="TaskId"/>
        /// </summary>
        /// <param name="taskId"><see cref="TaskId"/> of the task to stop</param>
        void Stop(TaskId taskId);

        /// <summary>
        /// Pause a task by its <see cref="TaskId"/>
        /// </summary>
        /// <param name="taskId"><see cref="TaskId"/> of the task to stop</param>
        void Pause(TaskId taskId);
    }
}
