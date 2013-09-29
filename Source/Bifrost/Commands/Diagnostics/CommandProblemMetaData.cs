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
using Bifrost.Diagnostics;

namespace Bifrost.Commands.Diagnostics
{
    /// <summary>
    /// Represents the metadata for a <see cref="IProblem"/> generated for a <see cref="ICommand"/>
    /// </summary>
    public class CommandProblemMetaData
    {
        /// <summary>
        /// Get the metadata for a specific type
        /// </summary>
        /// <param name="type">Type to get from</param>
        /// <returns>The metadata associated with the type for a problem</returns>
        public static object From(Type type)
        {
            return new { Name = type.Name, Namespace = type.Namespace };
        }
    }
}
