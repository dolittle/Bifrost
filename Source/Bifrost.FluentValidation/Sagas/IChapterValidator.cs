/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Conventions;
using Bifrost.Validation;

namespace Bifrost.FluentValidation.Sagas
{
    /// <summary>
    /// Marker interface for a Saga <see href="IChapter">Chapter</see> validator.
    /// </summary>
    /// <remarks>
    /// Types inheriting from this interface and also <see cref="ICanValidate{T}"/> will be automatically registered.
    /// You most likely want to subclass <see cref="ChapterValidator{T}"/>.
    /// </remarks>
    public interface IChapterValidator : IConvention
    {
    }
}
