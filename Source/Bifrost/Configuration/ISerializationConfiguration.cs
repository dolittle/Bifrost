/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Defines the configuration for serialization
	/// </summary>
	public interface ISerializationConfiguration: IConfigurationElement
	{
		/// <summary>
		/// Gets or sets the type of serializer to use throughout the system
		/// </summary>
		Type SerializerType { get; set; }
	}
}

