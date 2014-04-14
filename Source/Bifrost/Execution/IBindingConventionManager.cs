#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
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
	/// Defines a manager for binding conventions
	/// </summary>
    public interface IBindingConventionManager
    {
		/// <summary>
		/// Add a convention by type
		/// </summary>
		/// <param name="type">Type of convention to add</param>
		/// <remarks>
		/// The type must implement the <see cref="IBindingConvention"/>
		/// </remarks>
        void Add(Type type);

		/// <summary>
		/// Add a convention by type generically
		/// </summary>
		/// <typeparam name="T">Type of convention to add</typeparam>
        void Add<T>() where T : IBindingConvention;

		/// <summary>
		/// Initialize system
		/// </summary>
        void Initialize();

		/// <summary>
		/// Discover bindings and initialize
		/// </summary>
        void DiscoverAndInitialize();
    }
}
