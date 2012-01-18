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
using System.Linq;

namespace Bifrost.Views
{
	/// <summary>
	/// Defines a repository that can be queried against
	/// </summary>
	/// <typeparam name="T">Type that can be queried against</typeparam>
	public interface IView<T>
	{
		/// <summary>
		/// Gets a queryable that can be queried against
		/// </summary>
		IQueryable<T> Query { get; }

        /// <summary>
        /// Gets a single instance based on Id
        /// </summary>
        /// <param name="id">Id of instance to get</param>
        /// <returns>The instance found - null if not found</returns>
	    T Get(Guid id);
	}
}
