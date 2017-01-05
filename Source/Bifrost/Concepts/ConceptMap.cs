/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Bifrost.Concepts
{
    /// <summary>
    /// Maps a concept type to the underlying primitive type
    /// </summary>
    public static class ConceptMap
    {
        static readonly ConcurrentDictionary<Type, Type> _cache = new ConcurrentDictionary<Type, Type>();

        /// <summary>
        /// Get the type of the value in a <see cref="ConceptAs{T}"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> to get value type from</param>
        /// <returns>The type of the <see cref="ConceptAs{T}"/> value</returns>
        public static Type GetConceptValueType(Type type)
        {
            return _cache.GetOrAdd(type, GetPrimitiveType);
        }

        static Type GetPrimitiveType(Type type)
        {
            var conceptType = type;
            for(;;) 
            {
                if( conceptType == typeof(ConceptAs<>) ) break;
                var typeProperty = conceptType.GetTypeInfo().GetProperty("UnderlyingType");
                if( typeProperty != null ) {
                    var underlyingType = (Type) typeProperty.GetValue(null);
                    return underlyingType;
                }
                if( conceptType == typeof(object)) break;

                conceptType = conceptType.GetTypeInfo().BaseType;
            }

            return null;
        }
    }
}