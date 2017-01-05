/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Serialization
{
    /// <summary>
    /// Represents the options for serialization
    /// </summary>
	public class SerializationOptions
	{
        /// <summary>
        /// A func that gets called during serialization of properties to decide 
        /// </summary>
		public Func<Type, string, bool>	ShouldSerializeProperty = (t, p) => true;

        /// <summary>
        /// Gets or sets wether or not to use camel case for naming of properties
        /// </summary>
        public bool UseCamelCase { get; set; }

        /// <summary>
        /// Gets or sets wether or not to include type names during serialization
        /// </summary>
        public bool IncludeTypeNames { get; set; }
	}
}