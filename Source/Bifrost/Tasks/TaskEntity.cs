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
using System.Collections.Generic;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents an enity of a <see cref="Task"/> that can be persisted
    /// </summary>
    public class TaskEntity
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TaskEntity"/>
        /// </summary>
        public TaskEntity()
        {
            State = new Dictionary<string, string>();
        }


        /// <summary>
        /// Gets or sets the Id of the <see cref="Task"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the <see cref="Task"/>
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the current operation within the <see cref="Task"/>
        /// </summary>
        public int CurrentOperation { get; set; }

        /// <summary>
        /// Gets or sets any state that exists explicitly on the custom <see cref="Task"/>
        /// </summary>
        public IDictionary<string, string> State { get; private set; }
    }
}
