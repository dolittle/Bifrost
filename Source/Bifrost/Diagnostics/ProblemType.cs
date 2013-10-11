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

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Represents a type of problem, its underlying type is an identifier that should be unique per <see cref="ProblemType">problem type</see>
    /// </summary>
    public class ProblemType
    {
        /// <summary>
        /// Gets the unique identifier for the <see cref="ProblemType"/>
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the description of the problem type
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the severity of the problem
        /// </summary>
        public ProblemSeverity Severity { get; private set; }

        /// <summary>
        /// Creates a new <see cref="ProblemType"/> from a given id and description
        /// </summary>
        /// <param name="id">Id in the form of a valid <see cref="Guid"/> string representation</param>
        /// <param name="description">Description of the <see cref="ProblemType">problem type</see></param>
        /// <param name="severity">The <see cref="ProblemSeverity">severity</see> of the problem type</param>
        /// <returns>An <see cref="ProblemType"/> instance</returns>
        public static ProblemType Create(string id, string description, ProblemSeverity severity)
        {
            return new ProblemType { Id = Guid.Parse(id), Description = description, Severity = severity };
        }
    }
}
