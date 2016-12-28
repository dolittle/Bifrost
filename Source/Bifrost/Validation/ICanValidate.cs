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
using System.Collections.Generic;

namespace Bifrost.Validation
{
    /// <summary>
    /// Defines the behavior of being able to do validation
    /// </summary>
    public interface ICanValidate
    {
        /// <summary>
        /// Validates that the object is in a valid state.
        /// </summary>
        /// <param name="target">The target to validate</param>
        /// <returns>A collection of ValidationResults.  An empty collection indicates a valid command.</returns>
        IEnumerable<ValidationResult> ValidateFor(object target);
    }


	/// <summary>
	/// Defines the behavior of being able to do validation
	/// </summary>
	/// <typeparam name="T">Type it can validate</typeparam>
    public interface ICanValidate<in T> : ICanValidate
    {
        /// <summary>
        /// Validates that the object is in a valid state.
        /// </summary>
        /// <param name="target">The target to validate</param>
        /// <returns>A collection of ValidationResults.  An empty collection indicates a valid command.</returns>
        IEnumerable<ValidationResult> ValidateFor(T target);
    }
}