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
using Bifrost.Events;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Defines the configuration for events
	/// </summary>
    public interface IEventsConfiguration
    {
		/// <summary>
		/// Gets or sets the type of repository for events
		/// </summary>
        Type RepositoryType { get; set; }

        /// <summary>
        /// Add a <see cref="IEventStoreChangeNotifier"/> type for the configuration
        /// </summary>
        /// <param name="type"></param>
        void AddEventStoreChangeNotifier(Type type);

		/// <summary>
		/// Initialize the configuration
		/// </summary>
		/// <param name="configure"><see cref="IConfigure"/> instance to configure</param>
        void Initialize(IConfigure configure);
    }
}