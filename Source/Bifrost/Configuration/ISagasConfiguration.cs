/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Defines the configuration for sagas
	/// </summary>
	public interface ISagasConfiguration : IConfigurationElement, IHaveStorage
	{
		/// <summary>
		/// Gets or sets the type of librarian to use for sagas
		/// </summary>
		Type LibrarianType { get; set; }
	}
}