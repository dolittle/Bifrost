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
using System.Globalization;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
	/// <summary>
	/// Defines the configuration for Bifrost
	/// </summary>
	public interface IConfigure 
	{
		/// <summary>
		/// Gets the type of logger to use
		/// </summary>
		Type LoggerType { get; }

		/// <summary>
		/// Gets the container that is used
		/// </summary>
		IContainer Container { get; }

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
		/// Gets the manager that manages applications
		/// </summary>
		IApplicationManager ApplicationManager { get; }

		/// <summary>
		/// Gets the current <see cref="IApplication"/>
		/// </summary>
		IApplication Application { get; }

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
        BindingLifecycle DefaultObjectLifecycle { get; }

		/// <summary>
		/// Sets a specific configuration source
		/// </summary>
		/// <param name="configurationSource"><see cref="IConfigurationSource"/> to set</param>
		void ConfigurationSource(IConfigurationSource configurationSource);

		/// <summary>
		/// Initializes Bifrost after configuration
		/// </summary>
		void Initialize();
	}
}