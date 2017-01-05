/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Commands
{
	/// <summary>
	/// Represents a <see cref="ICommand"/>
	/// </summary>
    public partial class Command : ICommand
    {
		/// <summary>
		/// Initializes a new instance of <see cref="Command"/>
		/// </summary>
        public Command()
        {
            Id = Guid.NewGuid();
        }

#pragma warning disable 1591 // Xml Comments
		public Guid Id { get; set; }
#pragma warning restore 1591 // Xml Comments
	}
}
