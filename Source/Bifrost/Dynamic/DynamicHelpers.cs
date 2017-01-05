/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Dynamic
{
    /// <summary>
    /// Provides a set of helper methods for working with dynamic types
    /// </summary>
    public class DynamicHelpers
    {
        /// <summary>
        /// Populate a dynamic object, typically something like a <see cref="System.Dynamic.ExpandoObject"/>
        /// </summary>
        /// <param name="target">Target object that will receive all properties and values from source</param>
        /// <param name="source">Source object containing all properties with values - this can in fact be any type, including an anonymous one</param>
        public static void Populate(dynamic target, dynamic source)
        {
            var dictionary = target as IDictionary<string, object>;

            foreach (var property in source.GetType().GetProperties())
                dictionary[property.Name] = property.GetValue(source, null);
        }
    }
}
