/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Validation;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Validates that the chapter is valid and conforms to specified business rules
    /// </summary>
    public interface IChapterValidationService
    {
        /// <summary>
        /// Validate the chapter
        /// </summary>
        /// <param name="chapter">Instance to be validated</param>
        /// <returns>A collection of ValidationResults that indicate any invalid properties / rules</returns>
        IEnumerable<ValidationResult> Validate(IChapter chapter);
    }
}