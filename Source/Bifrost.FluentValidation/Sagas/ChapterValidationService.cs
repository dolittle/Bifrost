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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Execution;
using Bifrost.Sagas;

namespace Bifrost.FluentValidation.Sagas
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
        public IEnumerable<ValidationResult> Validate(IChapter chapter)
        {
            var validator = _chapterValidatorProvider.GetValidatorFor(chapter);
            
            return validator.ValidateFor(chapter);
        }
#pragma warning restore 1591 // Xml Comments
    }
}