/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Commands
{
	/// <summary>
	/// Defines the basic command
	/// </summary>
	public partial interface ICommand
	{
		/// <summary>
		/// Gets or sets the Id of the object the command will apply to
		/// </summary>
        Guid Id { get; set; }
	}
}
