/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Reflection;
using Bifrost.Configuration.Assemblies;
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
        /// Gets or sets the name of the currently running system
        /// </summary>
        string SystemName { get; set; }

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
        /// Gets the configuration for <see cref="ICallContext"/>
        /// </summary>
        ICallContextConfiguration CallContext { get; }

        /// <summary>
        /// Gets the configuration for the <see cref="IExecutionContext"/>
        /// </summary>
        IExecutionContextConfiguration ExecutionContext { get; }

        /// <summary>
        /// Gets the configuration for security
        /// </summary>
        ISecurityConfiguration Security { get; }

        /// <summary>
        /// Gets the configuration for assemblies and how they are treated
        /// </summary>
        AssembliesConfiguration Assemblies { get; }
		
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
        BindingLifecycle DefaultLifecycle { get; set; }

		/// <summary>
		/// Initializes Bifrost after configuration
		/// </summary>
		void Initialize();
	}
}