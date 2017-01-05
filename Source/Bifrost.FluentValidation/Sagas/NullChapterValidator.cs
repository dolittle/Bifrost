/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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