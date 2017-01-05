/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Execution;
using Bifrost.Sagas;
using Bifrost.Validation;

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