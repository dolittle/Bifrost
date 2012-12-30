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
        public Dictionary<string, string> State { get; set; }
    }
}
