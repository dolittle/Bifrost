#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
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
using System.Collections.Generic;

namespace Bifrost.Execution
{
	/// <summary>
	/// Discovers types based upon basetypes
	/// </summary>
	public interface ITypeDiscoverer
	{
        /// <summary>
        /// Returns all discovered types
        /// </summary>
        /// <returns><see cref="IEnumerable{Type}"/> with all the types discovered</returns>
	    IEnumerable<Type> GetAll();

		/// <summary>
		/// Find a single implementation of a basetype
		/// </summary>
		/// <typeparam name="T">Basetype to find for</typeparam>
		/// <returns>Type found</returns>
		/// <remarks>
		/// If the base type is an interface, it will look for any types implementing the interface.
		/// If it is a class, it will find anyone inheriting from that class
		/// </remarks>
		/// <exception cref="ArgumentException">If there is more than one instance found</exception>
		Type FindSingle<T>();

		/// <summary>
		/// Find multiple implementations of a basetype
		/// </summary>
		/// <typeparam name="T">Basetype to find for</typeparam>
		/// <returns>All types implementing or inheriting from the given basetype</returns>
		/// <remarks>
		/// If the base type is an interface, it will look for any types implementing the interface.
		/// If it is a class, it will find anyone inheriting from that class
		/// </remarks>
		Type[] FindMultiple<T>();

		/// <summary>
		/// Find a single implementation of a basetype
		/// </summary>
		/// <param name="type">Basetype to find for</param>
		/// <returns>Type found</returns>
		/// <remarks>
		/// If the base type is an interface, it will look for any types implementing the interface.
		/// If it is a class, it will find anyone inheriting from that class
		/// </remarks>
		/// <exception cref="ArgumentException">If there is more than one instance found</exception>
		Type FindSingle(Type type);

		/// <summary>
		/// Find multiple implementations of a basetype
		/// </summary>
		/// <param name="type">Basetype to find for</param>
		/// <returns>All types implementing or inheriting from the given basetype</returns>
		/// <remarks>
		/// If the base type is an interface, it will look for any types implementing the interface.
		/// If it is a class, it will find anyone inheriting from that class
		/// </remarks>
		Type[] FindMultiple(Type type);
	}
}