#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use file except in compliance with the License.
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

namespace Bifrost.Concurrency
{
    /// <summary>
    /// Defines a scheduler for scheduling operations
    /// </summary>
    public interface IScheduler
    {
        /// <summary>
        /// Start an <see cref="Action"/> on a seperate thread
        /// </summary>
        /// <param name="action"><see cref="Action"/> to perform</param>
        /// <param name="actionDone">Optional <see cref="Action"/> to call when it is done</param>
        Guid Start(Action action, Action actionDone = null);

        /// <summary>
        /// Start an <see cref="Action{T}"/> on a seperate thread with state passed along to the <see cref="Action{T}"/>
        /// </summary>
        /// <param name="action"><see cref="Action{T}"/> to perform</param>
        /// <param name="objectState">State to pass along to the action</param>
        /// <param name="actionDone">Optional <see cref="Action{T}"/> to call when it is done</param>
        Guid Start<T>(Action<T> action, T objectState, Action<T> actionDone = null);

        /// <summary>
        /// Stop a scheduled <see cref="Action"/>
        /// </summary>
        /// <param name="id"></param>
        void Stop(Guid id);
    }
}
