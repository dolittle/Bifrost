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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Commands;

namespace Bifrost.Validation
{
    /// <summary>
    /// Represent a null or non-existant validator.
    /// </summary>
    /// <remarks>
    /// Always returns an empty validation result collection.
    /// </remarks>
    public class NullCommandBusinessValidator : ICanValidate<ICommand>, ICommandBusinessValidator
    {
#pragma warning disable 1591 // Xml Comments
        public IEnumerable<ValidationResult> ValidateFor(ICommand command)
        {
            return new ValidationResult[0];
        }

        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return new ValidationResult[0];
        }
#pragma warning restore 1591 // Xml Comments
    }
}