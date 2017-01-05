/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Concepts;

namespace Bifrost.Extensions
{
    /// <summary>
    /// Provides a set of extension methods to <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Convert a string into a camel cased string
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns>Converted string</returns>
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length == 1)
                    return str.ToLowerInvariant();

                var firstLetter = str[0].ToString().ToLowerInvariant();
                return firstLetter + str.Substring(1);
            }
            return str;
        }

        /// <summary>
        /// Convert a string into a pascal cased string
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns>Converted string</returns>
        public static string ToPascalCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length == 1)
                    return str.ToUpperInvariant();

                var firstLetter = str[0].ToString().ToUpperInvariant();
                return firstLetter + str.Substring(1);
            }
            return str;
        }

        /// <summary>
        /// Convert a string into the desired type
        /// </summary>
        /// <param name="input">the string to parse</param>
        /// <param name="type">the desired type</param>
        /// <returns>value as the desired type</returns>
        public static object ParseTo(this string input, Type type)
        {
            if (type == typeof(Guid)) {
                Guid result;
                if (Guid.TryParse(input, out result)) return result;
                return Guid.Empty;
            }

            if (type.IsConcept())
            {
                var primitiveType = type.GetConceptValueType();
                var primitive = ParseTo(input, primitiveType);
                return ConceptFactory.CreateConceptInstance(type, primitive);
            }

            return Convert.ChangeType(input, type, null);           
        }
    }
}
