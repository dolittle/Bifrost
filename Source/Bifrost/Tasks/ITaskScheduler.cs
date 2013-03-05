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
