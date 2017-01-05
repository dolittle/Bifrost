/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Entities;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Defines a configuration set for an <see cref="IEntityContext{T}">EntityContext</see> implementation
	/// </summary>
	public interface IEntityContextConfiguration
	{
		/// <summary>
		/// Gets the EntityContext type
		/// </summary>
		Type EntityContextType { get; }

		/// <summary>
		/// Gets or sets the connection information for the entity context
		/// </summary>
		IEntityContextConnection Connection { get; set; }
	}
}