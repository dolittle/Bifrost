using System;

namespace Bifrost.Extensions
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeInfo"/>
    /// </summary>
    /// <typeparam name="T">Type it holds info for</typeparam>
    public class TypeInfo<T> : ITypeInfo
    {
        /// <summary>
        /// Gets a singleton instance of the TypeInfo
        /// </summary>
        public static readonly TypeInfo<T> Instance = new TypeInfo<T>();

        TypeInfo()
        {
            var type = typeof(T); 
            HasDefaultConstructor = 
                type.IsValueType ||
                type.GetConstructor(new Type[0]) != null ;
        }

#pragma warning disable 1591 // Xml Comments
        public bool HasDefaultConstructor { get; private set; }
#pragma warning restore 1591 // Xml Comments

    }
}
