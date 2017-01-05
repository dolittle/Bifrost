/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Sagas
{
	/// <summary>
	/// The exception that is thrown if a transition between two chapters are not allowed
	/// </summary>
    public class ChapterTransitionNotAllowedException : Exception
    {
		/// <summary>
		/// Initializes an instance of <see cref="ChapterTransitionNotAllowedException"/>
		/// </summary>
		/// <param name="from">From <see cref="IChapter"/></param>
		/// <param name="to">To <see cref="IChapter"/></param>
        public ChapterTransitionNotAllowedException(Type from, Type to) : base(string.Format("Can't transition from {0} to {1} - not allowed", from.Name, to.Name)) {}
    }
}