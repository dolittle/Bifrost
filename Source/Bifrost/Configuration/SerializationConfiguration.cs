/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Serialization;
using System;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="ISerializationConfiguration"/>
    /// </summary>
	public class SerializationConfiguration : ISerializationConfiguration
    {
#pragma warning disable 1591 // Xml Comments
        public Type SerializerType { get; set; }

        public void Initialize (IContainer container)
		{
			if( SerializerType != null )
				container.Bind<ISerializer>(SerializerType, BindingLifecycle.Singleton);
		}
#pragma warning restore 1591 // Xml Comments
    }
}

