/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Sagas;
using Bifrost.Validation;

namespace Bifrost.FluentValidation.Sagas
{
    /// <summary>
    /// Defines a provider that returns chapter-specific validators
    /// </summary>
    public interface IChapterValidatorProvider
    {
        /// <summary>
        /// Retrieves an validator specific to the chapter
        /// </summary>
        /// <param name="chapter">Chapter to be validates</param>
        /// <returns>Returns specific validator or a NullValidator if no validator is available</returns>
        ICanValidate GetValidatorFor(IChapter chapter);

        /// <summary>
        /// Retrieves an validator specific to the chapter type
        /// </summary>
        /// <param name="type">Type of the Chapter to be validates</param>
        /// <returns>Returns specific validator or a NullValidator if no validator is available</returns>
        ICanValidate GetValidatorFor(Type type);
    }
}