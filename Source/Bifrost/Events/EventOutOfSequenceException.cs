/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Events
{
	/// <summary>
	/// The exception that is thrown when an <see cref="IEvent"/> is out of sequence in an <see cref="EventStream"/>
	/// </summary>
    public class EventOutOfSequenceException : ArgumentException
    {
    }
}
