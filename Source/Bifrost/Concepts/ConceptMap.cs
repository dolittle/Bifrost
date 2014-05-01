using System;
using System.Collections.Concurrent;
using System.Reflection;
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
#if(NETFX_CORE)
            var typeInfo = conceptType.GetTypeInfo();
            if (conceptType == null || !typeInfo.IsGenericType) return null;
            var genericArgumentType = typeInfo.GenericTypeArguments[0];
#else
            if (conceptType == null || !conceptType.IsGenericType) return null;
            var genericArgumentType = conceptType.GetGenericArguments()[0];
#endif
            return genericArgumentType.HasInterface(typeof(IEquatable<>)) ? genericArgumentType : null;
        }

        static Type FindConceptAsType(Type type)
        {
            var openGenericType = typeof (ConceptAs<>);
            var typeToCheck = type;
            while (typeToCheck != null && typeToCheck != typeof(object))
            {
#if(NETFX_CORE)
                var currentType = typeToCheck.GetTypeInfo().IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;
#else
                var currentType = typeToCheck.IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;
#endif
                if (openGenericType == currentType)
                {
                    return typeToCheck;
                }

#if(NETFX_CORE)
                typeToCheck = typeToCheck.GetTypeInfo().BaseType;
#else
                typeToCheck = typeToCheck.BaseType;
#endif
            }
            return null;
        }
    }
}