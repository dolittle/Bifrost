#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
    /// Defines an identifier for <see cref="Task">Tasks</see>
    /// </summary>
    public struct TaskId
    {
        /// <summary>
        /// Create a new <see cref="TaskId"/>
        /// </summary>
        /// <returns></returns>
        public static TaskId New()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the actual value
        /// </summary>
        public Guid Value { get; set; }

        /// <summary>
        /// Implicitly convert from <see cref="TaskId"/> to <see cref="Guid"/>
        /// </summary>
        /// <param name="taskId"><see cref="TaskId"/> to convert from</param>
        /// <returns>The <see cref="Guid"/> from the <see cref="TaskId"/></returns>
        public static implicit operator Guid(TaskId taskId)
        {
            return taskId.Value;
        }


        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="TaskId"/>
        /// </summary>
        /// <param name="taskId"><see cref="Guid"/> to convert from</param>
        /// <returns>The <see cref="TaskId"/> created from the <see cref="Guid"/> </returns>
        public static implicit operator TaskId(Guid taskId)
        {
            return new TaskId { Value = taskId };
        }
    }
}
