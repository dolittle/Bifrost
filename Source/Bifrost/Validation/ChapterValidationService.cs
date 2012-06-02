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
using Bifrost.Execution;
using Bifrost.Sagas;

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents a <see cref="IChapterValidationService">IChapterValidationService</see>
    /// </summary>
    [Singleton]
    public class ChapterValidationService : IChapterValidationService
    {
        readonly IChapterValidatorProvider _chapterValidatorProvider;

        /// <summary>
        /// Initializes an instance of <see cref="ChapterValidationService"/>
        /// </summary>
        /// <param name="chapterValidatorProvider">A <see cref="IChapterValidatorProvider"/> to use for getting validators from</param>
        public ChapterValidationService(IChapterValidatorProvider chapterValidatorProvider)
        {
            _chapterValidatorProvider = chapterValidatorProvider;
        }

#pragma warning disable 1591 // Xml Comments

        public IEnumerable<ValidationResult> ValidateTransistionTo<T>(IChapter chapter)
        {
            var transitionValidator = _chapterValidatorProvider.GetValidatorForTransitionTo<T>(chapter);

            var validator =  transitionValidator is NullChapterValidator 
                            ? _chapterValidatorProvider.GetValidatorFor(chapter) 
                            : transitionValidator;

            return validator.ValidateChapter(chapter);
        }
#pragma warning restore 1591 // Xml Comments
    }
}