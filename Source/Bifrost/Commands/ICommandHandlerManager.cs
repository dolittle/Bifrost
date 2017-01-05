/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Commands
{
	/// <summary>
	/// Defines the functionality for a manager that handles commands
	/// 
	/// Handles a <see cref="ICommand">command</see> by calling any
	/// command handlers that can handle the specific command
	/// </summary>
	public interface ICommandHandlerManager
	{
		/// <summary>
		/// Handle a command
		/// </summary>
		/// <param name="command"><see cref="ICommand">Command</see> to handle</param>
		void Handle(ICommand command);
	}
}