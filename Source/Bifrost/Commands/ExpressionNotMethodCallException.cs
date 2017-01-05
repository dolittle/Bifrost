/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Commands
{
	/// <summary>
	/// The exception that is thrown when an expression is not a method call
	/// </summary>
    public class ExpressionNotMethodCallException : Exception
    {
		/// <summary>
		/// Initializes a new instance of <see cref="ExpressionNotMethodCallException"/>
		/// </summary>
		/// <param name="message">Message with details for the exception</param>
        public ExpressionNotMethodCallException(string message) : base(message) {}
    }
}