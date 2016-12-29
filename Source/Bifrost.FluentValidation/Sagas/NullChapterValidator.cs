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
using Bifrost.Sagas;
using Bifrost.Validation;

namespace Bifrost.FluentValidation.Sagas
{
    /// <summary>
    /// Represent a null or non-existant validator.
    /// </summary>
    /// <remarks>
    /// Always returns an empty validation result collection.
    /// </remarks>
    public class NullChapterValidator : ICanValidate<IChapter>, IChapterValidator
    {
#pragma warning disable 1591 // Xml Comments
        public IEnumerable<ValidationResult> ValidateFor(IChapter chapter)
        {
            return new ValidationResult[0];
        }

        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object chapter)
        {
            return new ValidationResult[0];
        }

#pragma warning restore 1591 // Xml Comments
    }
}