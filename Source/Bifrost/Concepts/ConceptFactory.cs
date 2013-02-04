using System;

namespace Bifrost.Concepts
{
    /// <summary>
    /// Factory to create an instance of an<see cref="ConceptAs"/> from the Type and Underlying value.
    /// </summary>
    public class ConceptFactory
    {
        /// <summary>
        /// Creates an instance of a <see cref="ConceptAs"/> given the type and underlying value.
        /// </summary>
        /// <param name="type">Type of the ConceptAs to create</param>
        /// <param name="value">Value to give to this instance</param>
        /// <returns>An instance of a ConceptAs with the specified value</returns>
        public static object CreateConceptInstance(Type type, object value)
        {
            var instance = Activator.CreateInstance(type);
            var genericArgumentType = type.BaseType.GetGenericArguments()[0];
            if (genericArgumentType == typeof(Guid))
                value = Guid.Parse(value.ToString());

            type.GetProperty("Value").SetValue(instance, value, null);
            return instance;
        }
    }
}