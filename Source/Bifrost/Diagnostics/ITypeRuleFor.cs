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
using System;
namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Defines a rule that runs diagnostics on a specific type
    /// </summary>
    /// <typeparam name="T">Type that the rule applies to</typeparam>
    public interface ITypeRuleFor<T>
    {
        /// <summary>
        /// Validate and report any problems
        /// </summary>
        /// <param name="type"><see cref="Type"/> to validate</param>
        /// <param name="problems"><see cref="Problems"/> to report back on, if any</param>
        void Validate(Type type, IProblems problems);
    }
}
