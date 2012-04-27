#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
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
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Bifrost.Commands;

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