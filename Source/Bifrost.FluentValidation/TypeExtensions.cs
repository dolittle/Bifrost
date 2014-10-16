using System;
using Bifrost.Extensions;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Contains Type extension specific to validation
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Check if a type is a ModelRuleProperty{T}
        /// </summary>
        /// <returns>True if this type is a ModelRuleProperty, otherwise false</returns>
        public static bool IsModelRuleProperty(this Type type)
        {
            return type.IsDerivedFromOpenGeneric(typeof (ModelRule<>));
        }
    }
}