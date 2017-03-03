/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Sagas;
using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.FluentValidation.Sagas
{
    /// <summary>
    /// Base class to inherit from for validation of a Chapter.
    /// </summary>
    /// <typeparam name="T">Concrete type of the Chapter to validate</typeparam>
    public abstract class ChapterValidator<T> : AbstractValidator<T>, ICanValidate<T>, IChapterValidator
        where T : class, IChapter
    {
#pragma warning disable 1591 // Xml Comments
        public virtual IEnumerable<ValidationResult> ValidateFor(T chapter)
        {
            var result = Validate(chapter);
            return from error in result.Errors
                select new ValidationResult(error.ErrorMessage, new[] { error.PropertyName });
        }

        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return ValidateFor((T)target);
        }

#pragma warning restore 1591 // Xml Comments
    }
}
