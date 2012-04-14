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

namespace Bifrost.Execution
{
	/// <summary>
	/// Defines the basic functionality for a convention that can be applied
	/// to bindings for a <see cref="IContainer"/>
	/// </summary>
	public interface IBindingConvention
	{
		/// <summary>
		/// Checks wether or not a given <see cref="Type">Service</see> can be resolved by the convention
		/// </summary>
        /// <param name="container">Container to resolve binding for</param>
		/// <param name="service">Service that needs to be resolved</param>
		/// <returns>True if it can resolve it, false if not</returns>
		bool CanResolve(IContainer container, Type service);

		/// <summary>
		/// Resolve a <see cref="Type">Service</see>
		/// </summary>
		/// <param name="container">Container to resolve binding for</param>
        /// <param name="service">Service that needs to be resolved</param>
		void Resolve(IContainer container, Type service);
	}
}