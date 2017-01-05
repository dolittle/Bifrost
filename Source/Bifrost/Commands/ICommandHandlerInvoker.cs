/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Commands
{
	/// <summary>
	/// Invokes a command for a command handler type
	/// </summary>
	/// <remarks>
	/// Typically, the default invoker handles the generic
	/// <see cref="IHandleCommands">command handlers</see>
	/// </remarks>
	public interface ICommandHandlerInvoker
	{
		/// <summary>
		/// Try to handle a command
		/// 
		/// If it can handle it, it should handle it - and return true
		/// if it handled it and false if not
		/// </summary>
		/// <param name="command"><see cref="ICommand">Command to handle</see></param>
		/// <returns>True if it handled it, false if not</returns>
		bool TryHandle(ICommand command);
	}
}