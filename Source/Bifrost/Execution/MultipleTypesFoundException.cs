/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{
	/// <summary>
	/// The exception that is thrown when multiple types are found and not allowed
	/// </summary>
    public class MultipleTypesFoundException : ArgumentException
    {
		/// <summary>
		/// Initializes an instance of <see cref="MultipleTypesFoundException"/>
		/// </summary>
        public MultipleTypesFoundException() {}

		/// <summary>
		/// Initializes an instance of <see cref="MultipleTypesFoundException"/>
		/// </summary>
		/// <param name="message">Message with details about the exception</param>
        public MultipleTypesFoundException(string message) : base(message) {}
    }
}