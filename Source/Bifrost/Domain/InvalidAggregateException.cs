/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Domain
{
	/// <summary>
	/// The exception that is thrown when there is something invalid with
	/// an <see cref="AggregateRoot">AggregatedRoot</see>
	/// </summary>
	public class InvalidAggregateException : ArgumentException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidAggregateException">InvalidAggregateException</see> class
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		public InvalidAggregateException(string message)
			: base(message)
		{
		}
	}
}
