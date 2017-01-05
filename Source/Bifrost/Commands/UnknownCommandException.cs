/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Commands
{
    /// <summary>
    /// The exception that is thrown when a command is not known by its name in the system
    /// </summary>
	public class UnknownCommandException : ArgumentException
	{
        /// <summary>
        /// Initializes a new instance of <see cref="UnknownCommandException"/>
        /// </summary>
        /// <param name="name"></param>
		public UnknownCommandException (string name) : base("There is no command called : "+name)
		{
		}
	}
}

