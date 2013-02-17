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

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a persisted <see cref="CommandContext">CommandContext</see>
    /// </summary>
    public class PersistentCommandContext
    {
        /// <summary>
        /// Gets or sets the Id of the command
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the command - assembly fully qualified name
        /// </summary>
        public string CommandType { get; set; }

        /// <summary>
        /// Gets or sets the serialized version of the actual command
        /// </summary>
        public string SerializedCommand { get; set; }

        /// <summary>
        /// Gets or sets the origin of the command
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the identity of the user or system that caused the command
        /// </summary>
        public string CausedBy { get; set; }
    }
}