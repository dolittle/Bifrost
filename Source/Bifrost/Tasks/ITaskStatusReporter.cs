#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
