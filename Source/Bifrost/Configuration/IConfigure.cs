#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using System.Globalization;
using System.Reflection;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Defines the configuration for Bifrost
	/// </summary>
	public interface IConfigure 
	{
		/// <summary>
		/// Gets the container that is used
		/// </summary>
		IContainer Container { get; }

        /// <summary>
        /// Gets the entry assembly for the application
        /// </summary>
        Assembly EntryAssembly { get; }

		/// <summary>
		/// Gets the configuration for commands
		/// </summary>
		ICommandsConfiguration Commands { get; }

		/// <summary>
		/// Gets the configuration for events.
        /// Supports specific storage
		/// </summary>
		IEventsConfiguration Events { get; }

        /// <summary>
        /// Gets the configuration for <see cref="Bifrost.Tasks.Task">Tasks</see>
        /// Supports specific storage
        /// </summary>
        ITasksConfiguration Tasks { get; }

		/// <summary>
		/// Gets the configuration for views
		/// </summary>
		IViewsConfiguration Views { get; }

		/// <summary>
		/// Gets the convention manager for bindings
		/// </summary>
		IBindingConventionManager ConventionManager { get; }

		/// <summary>
		/// Gets the configuration for sagas
        /// Supports specific storage
		/// </summary>
		ISagasConfiguration Sagas { get; }

        /// <summary>
        /// Gets the configureation for serialization
        /// </summary>
        ISerializationConfiguration Serialization { get; }
        
        /// <summary>
        /// Gets the configureation for the applications default storage
        /// </summary>
        IDefaultStorageConfiguration DefaultStorage { get; }

        /// <summary>
        /// Gets the configuration for the frontend part of the application
        /// </summary>
        IFrontendConfiguration Frontend { get; }
		
		/// <summary>
		/// Gets or sets the <see cref="CultureInfo">culture</see> to use in Bifrost
		/// </summary>
		CultureInfo Culture { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="CultureInfo">UI culture</see> to use in Bifrost
		/// </summary>
		CultureInfo UICulture { get; set; }

        /// <summary>
        /// Gets or sets the default <see cref="BindingLifeCycle"/> for objects when created/managed by the <see cref="IContainer"/>
        /// </summary>
        BindingLifecycle DefaultObjectLifecycle { get; set; }

		/// <summary>
		/// Initializes Bifrost after configuration
		/// </summary>
		void Initialize();
	}
}