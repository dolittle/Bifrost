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
    public class SerializationOptions : ISerializationOptions
    {
        static readonly SerializationOptions DefaultOptions = new SerializationOptions(SerializationOptionsFlags.None);
        static readonly SerializationOptions CamelCaseOptions = new SerializationOptions(SerializationOptionsFlags.UseCamelCase);
        static readonly SerializationOptions IncludeTypeNamesOptions = new SerializationOptions(SerializationOptionsFlags.IncludeTypeNames);

        /// <summary>
        /// Gets the default serialization options that will serialize all properties without any special flags.
        /// </summary>
        public static ISerializationOptions Default { get { return DefaultOptions; } }

        /// <summary>
        /// Gets the camel case serialization options that will serialize all properties using camel case.
        /// </summary>
        public static ISerializationOptions CamelCase { get { return CamelCaseOptions; } }

        /// <summary>
        /// Gets the type names serialization options that will serialize all properties including type names.
        /// </summary>
        public static ISerializationOptions IncludeTypeNames { get { return IncludeTypeNamesOptions; } }

        /// <summary>
        /// Initializes a new instance of <see cref="SerializationOptions"/>
        /// </summary>
        /// <param name="flags">The serialization flags</param>
        /// <remarks>
        /// All instances of this class or subclasses must be immutable, because mapping from
        /// serialization options to contract resolvers are cached for performance reasons.
        /// </remarks>
        protected SerializationOptions(SerializationOptionsFlags flags)
        {
            Flags = flags;
        }

        /// <summary>
        /// Will always return true
        /// </summary>
        public virtual bool ShouldSerializeProperty(Type type, string propertyName)
        {
            return true;
        }

        /// <summary>
        /// Gets the serialization flags
        /// </summary>
        public SerializationOptionsFlags Flags { get; private set; }
    }
}