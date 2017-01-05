/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Sagas
{
	/// <summary>
	/// The exception that is thrown if a <see cref="IChapter"/> does not exist in a <see cref="ISaga"/>
	/// </summary>
	public class ChapterDoesNotExistException : Exception
	{
		
	}
}