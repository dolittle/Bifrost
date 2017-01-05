/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Defines the recorder for <see cref="ISaga"/>
    /// </summary>
    public interface ISagaNarrator
    {
        /// <summary>
        /// Begin a <see cref="ISaga"/>
        /// </summary>
        /// <typeparam name="T">Type of saga to begin</typeparam>
        /// <returns>An instance of the new saga</returns>
        T Begin<T>() where T : ISaga;

        /// <summary>
        /// Continue a <see cref="ISaga"/>
        /// </summary>
        /// <param name="id">Identifier of the saga</param>
        /// <returns>An instance of the <see cref="ISaga"/></returns>
        ISaga Continue(Guid id);

        /// <summary>
        /// Conclude a <see cref="ISaga"/>
        /// </summary>
        /// <param name="saga"></param>
        /// <returns></returns>
        /// <remarks>
        /// Conclusion means that the saga is in fact not available any more
        /// </remarks>
        SagaConclusion Conclude(ISaga saga);

        /// <summary>
        /// Transition to a <see cref="IChapter"/> by type
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IChapter"/> to transition to</typeparam>
        /// <param name="saga"><see cref="ISaga"/> to transition</param>
        /// <returns><see cref="ChapterTransition" /> Result of the transition attempt.  If successful, this will contain instance of the target <see cref="IChapter"/> that was transitioned to.  Else, the validation errors.</returns>
        /// <remarks>
        /// If the chapter does not exist it will create it
        /// </remarks>
        ChapterTransition TransitionTo<T>(ISaga saga) where T:IChapter;
    }
}