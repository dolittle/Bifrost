/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Sagas;

namespace Bifrost.Commands
{
	/// <summary>
	/// Defines a coordinator for coordinating commands coming into the system
	/// </summary>
	public partial interface ICommandCoordinator
	{
		/// <summary>
		/// Handle a command in the context of a saga
		/// </summary>
		/// <param name="saga"><see cref="ISaga"/> to handle in context of</param>
		/// <param name="command"><see cref="ICommand">command</see> to handle</param>
		/// <returns>
		/// Result from the handle.
		/// Within the result one can check if the handling was success or not
		/// </returns>
		CommandResult Handle(ISaga saga, ICommand command);

		/// <summary>
		/// Handle a command
		/// </summary>
		/// <param name="command"><see cref="ICommand">command</see> to handle</param>
		/// <returns>
		/// Result from the handle.
		/// Within the result one can check if the handling was success or not
		/// </returns>
		CommandResult Handle(ICommand command);
	}
}
