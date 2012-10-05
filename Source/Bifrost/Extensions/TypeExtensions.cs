#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Reflection;

namespace Bifrost.Extensions
{
	/// <summary>
	/// Provides a set of methods for working with <see cref="Type">types</see>
	/// </summary>
	public static class TypeExtensions
	{
#pragma warning disable 1591 // Xml Comments
        static ITypeInfo GetTypeInfo(Type type)
        {
            var typeInfoType = typeof(TypeInfo<>).MakeGenericType(type);
            return typeInfoType.GetField("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as ITypeInfo;
        }
#pragma warning restore 1591 // Xml Comments


        /// <summary>
        /// Check if a type has a default constructor that does not take any arguments
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>true if it has a default constructor, false if not</returns>
        public static bool HasDefaultConstructor(this Type type)
        {
            return GetTypeInfo(type).HasDefaultConstructor;
        }

		/// <summary>
		/// Check if a type implements a specific interface
		/// </summary>
		/// <typeparam name="T">Interface to check for</typeparam>
		/// <param name="type">Type to check</param>
		/// <returns>True if the type implements the interface, false if not</returns>
		public static bool HasInterface<T>(this Type type)
		{
		    var hasInterface = type.HasInterface(typeof (T));
			return hasInterface;
		}

        /// <summary>
        /// Check if a type implements a specific interface
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <param name="interfaceType">Interface to check for</param>
        /// <returns>True if the type implements the interface, false if not</returns>
        public static bool HasInterface(this Type type, Type interfaceType)
        {
            var hasInterface = type.GetInterface(interfaceType.FullName, false) != null;
            return hasInterface;
        }

	}
}