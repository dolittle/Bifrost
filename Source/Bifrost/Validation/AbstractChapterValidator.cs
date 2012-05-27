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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bifrost.Sagas;
using FluentValidation;

namespace Bifrost.Validation
{
    /// <summary>
    /// Base handling of validation of a Chapter
    /// </summary>
    /// <typeparam name="T">Type of Chapter to be validated</typeparam>
    public abstract class AbstractChapterValidator<T> : AbstractValidator<T> where T: class , IChapter
    {
#pragma warning disable 1591 // Xml Comments
        public virtual IEnumerable<ValidationResult> ValidateChapter(IChapter chapter)
        {
            var result = Validate(chapter as T);
            return from error in result.Errors
                   select new ValidationResult(error.ErrorMessage, new[] { error.PropertyName });
        }
#pragma warning restore 1591 // Xml Comments
    }
}