/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.JSON.Serialization;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
		public static IConfigure UsingJson(this ISerializationConfiguration serializationConfiguration) 
		{
			serializationConfiguration.SerializerType = typeof(Serializer);
			return Configure.Instance;
		}
    }
}
