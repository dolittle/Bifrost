/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Concepts;
using MongoDB.Bson.Serialization;

namespace Bifrost.Events.MongoDB
{
    /// <summary>
    /// Represents a <see cref="IBsonSerializationProvider"/> that is capable of getting the correct
    /// serializer to deal with <see cref="ConceptAs{T}"/>
    /// </summary>
    public class ConceptSerializationProvider : IBsonSerializationProvider
    {
        /// <inheritdoc/>
        public IBsonSerializer GetSerializer(Type type)
        {
            if (type.IsConcept())
                return new ConceptSerializer(type);

            return null;
        }
    }
}
