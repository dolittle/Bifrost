using System;
using System.Collections.Concurrent;
using Bifrost.Extensions;

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
            var conceptType = FindConceptAsType(type);
            if (conceptType == null || !conceptType.IsGenericType) return null;
            var genericArgumentType = conceptType.GetGenericArguments()[0];
            return genericArgumentType.HasInterface(typeof(IEquatable<>)) ? genericArgumentType : null;
        }

        static Type FindConceptAsType(Type type)
        {
            var openGenericType = typeof (ConceptAs<>);
            var typeToCheck = type;
            while (typeToCheck != null && typeToCheck != typeof(object))
            {
                var currentType = typeToCheck.IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;
                if (openGenericType == currentType)
                {
                    return typeToCheck;
                }
                typeToCheck = typeToCheck.BaseType;
            }
            return null;
        }
    }
}