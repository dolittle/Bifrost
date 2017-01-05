/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Sagas
{
	/// <summary>
	/// The exception that is thrown when a <see cref="ISaga"/> is not in a <see cref="IChapter"/>
	/// </summary>
    public class SagaNotInChapterException : Exception
    {
    }
}