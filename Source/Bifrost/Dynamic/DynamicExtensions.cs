/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using System.Dynamic;

namespace Bifrost.Dynamic
{
    /// <summary>
    /// Provides a set of extension methods for working with dynamic types
    /// </summary>
    public static class DynamicExtensions
    {
        /// <summary>
        /// Converts an object to a dynamic <see cref="ExpandoObject"/>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static dynamic AsExpandoObject(this object source)
        {
            var expando = new ExpandoObject();

            foreach (var property in source.GetType().GetTypeInfo().GetProperties())
                ((IDictionary<string,object>)expando)[property.Name] = property.GetValue(source, null);

            return expando;
        }
    }
}
