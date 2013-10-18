using System;
using System.Collections.Generic;
using Bifrost.Concepts;
using Bifrost.Extensions;

/// <summary>
/// Maps a concept type to the underlying primitive type
/// </summary>
public static class ConceptMap
{
    //SILVERLIGHT VERSION OF CONCURRENT DICTIONARY??
    static readonly Dictionary<Type, Type> _cache = new Dictionary<Type, Type>();
    static object lockobject = new object();

    /// <summary>
    /// Get the type of the value in a <see cref="ConceptAs{T}"/>
    /// </summary>
    /// <param name="type"><see cref="Type"/> to get value type from</param>
    /// <returns>The type of the <see cref="ConceptAs{T}"/> value</returns>
    public static Type GetConceptValueType(Type type)
    {
        Type t;
        lock (lockobject)
        {
            if(!_cache.TryGetValue(type, out t))
            {
                t = GetPrimitiveType(type);
                _cache.Add(type,t);
            }
        }
        return t;
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
        var openGenericType = typeof(ConceptAs<>);
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
