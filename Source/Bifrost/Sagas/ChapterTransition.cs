/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Commands;

namespace Bifrost.Sagas
{
	/// <summary>
	/// Represents the result of a transition between <see cref="IChapter">chapters</see> 
	/// </summary>
	/// <remarks>
	/// Since chapters work with commands, this result is inheriting from <see cref="CommandResult"/>
	/// </remarks>
    public class ChapterTransition : CommandResult
    {
		/// <summary>
		/// Gets or sets the <see cref="IChapter"/> that was transitioned to, if succeeded
		/// </summary>
        public IChapter TransitionedTo { get; set; }
    }
}