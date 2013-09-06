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
using Bifrost.Diagnostics;

namespace Bifrost.Commands.Diagnostics
{
    /// <summary>
    /// Represents the type of problems for a <see cref="ICommand"/> that can occur
    /// </summary>
    public class ProblemTypes : IProblemTypes
    {
        /// <summary>
        /// The <see cref="ProblemType"/> representing a command with too many properties on it
        /// </summary>
        public static ProblemType TooManyProperties = ProblemType.Create("FD763B06-E9CF-41CE-ABD2-30CAABBB9E36", "Too many properties on command - a command should be as focused as possible, reflecting a behavior in the system", ProblemSeverity.Warning);

        /// <summary>
        /// The <see cref="ProblemType"/> representing a command with complex types used for properties
        /// </summary>
        public static ProblemType ComplexTypes = ProblemType.Create("7D9BE91A-C8FD-4EB3-8391-15D6EFD57A46", "You should try to avoid having properties that are complex types, it could be a symptom of not reflecting your domain and breaking the single responsibility principle", ProblemSeverity.Warning);

        /// <summary>
        /// The <see cref="ProblemType"/> representing a command that has a base type not directly implementing <see cref="ICommand"/> or inheriting from <see cref="Command"/>
        /// </summary>
        public static ProblemType CommandInheritance = ProblemType.Create("B79A63AE-D827-420F-A4FD-A53E24FD4B9B", "Are you sure you want to create an inheritance tree? Chances are that you are creating a coupling and in the process losing what the command is doing and its intent as well", ProblemSeverity.Suggestion);
    }
}
