/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Commands
{
	/// <summary>
	/// The exception that is thrown when a command is not handled by any <see cref="IHandleCommands"/>
	/// </summary>
    public class UnhandledCommandException : ArgumentException
    {
		/// <summary>
		/// Initializes a new instance <see cref="UnhandledCommandException"/>
		/// </summary>
		/// <param name="command"><see cref="ICommand"/> that wasn't handled</param>
        public UnhandledCommandException(ICommand command) : base(string.Format("Command of type '{0}' was not handled",command.GetType()))
        {
            Command = command;
        }

		/// <summary>
		/// Gets the <see cref="ICommand"/> that wasn't handled
		/// </summary>
        public ICommand Command { get; private set; }
    }
}
