﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bifrost.Configuration.Defaults;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Diagnostics;
using Bifrost.Configuration.Assemblies;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents the default <see cref="IConfigure"/> type
    /// </summary>
    public class Configure : IConfigure
    {
        static readonly object InstanceLock = new object();

        /// <summary>
        /// Gets the static instance of <see cref="Configure"/>
        /// </summary>
        public static Configure Instance { get; private set; }


        Configure(IContainer container, BindingLifecycle defaultLifecycle,  IDefaultConventions defaultConventions, IDefaultBindings defaultBindings, AssembliesConfiguration assembliesConfiguration)
        {
            SystemName = "[Not Set]";

            AssembliesConfiguration = assembliesConfiguration;

            container.DefaultLifecycle = defaultLifecycle;
            container.Bind<IConfigure>(this);

            Container = container;

            defaultBindings.Initialize(Container);
            defaultConventions.Initialize();
            
            InitializeProperties();
        }

        /// <summary>
        /// Configure by letting Bifrost discover anything that implements the discoverable configuration interfaces
        /// </summary>
        /// <returns></returns>
        public static Configure DiscoverAndConfigure(Action<AssembliesConfigurationBuilder> assembliesConfigurationBuilderCallback = null, IEnumerable<ICanProvideAssemblies> additionalAssemblyProviders = null)
        {
            IContractToImplementorsMap contractToImplementorsMap;
            var assembliesConfigurationBuilder = BuildAssembliesConfigurationIfCallbackDefined(assembliesConfigurationBuilderCallback);

            contractToImplementorsMap = new ContractToImplementorsMap();
            var executingAssembly = typeof(Configure).GetTypeInfo().Assembly;
            contractToImplementorsMap.Feed(executingAssembly.GetTypes());
            var assemblySpecifiers = new AssemblySpecifiers(contractToImplementorsMap, new TypeFinder(), assembliesConfigurationBuilder.RuleBuilder);
            assemblySpecifiers.SpecifyUsingSpecifiersFrom(executingAssembly);

            var assemblyProviders = new List<ICanProvideAssemblies>
            {
#if(NET461)
                new AppDomainAssemblyProvider(),
#endif
                //new DefaultAssemblyProvider()
                new FileSystemAssemblyProvider(new FileSystem())
            };


            if (additionalAssemblyProviders != null) assemblyProviders.AddRange(additionalAssemblyProviders);

            var assembliesConfiguration = new AssembliesConfiguration(assembliesConfigurationBuilder.RuleBuilder);
            var assemblyProvider = new AssemblyProvider(
                assemblyProviders,
                new AssemblyFilters(assembliesConfiguration), 
                new AssemblyUtility(),
                assemblySpecifiers,
                contractToImplementorsMap);

            var assemblies = assemblyProvider.GetAll(); 
            
            var canCreateContainerType = DiscoverCanCreateContainerType(assemblies);
            ThrowIfCanCreateContainerNotFound(canCreateContainerType);
            ThrowIfCanCreateContainerDoesNotHaveDefaultConstructor(canCreateContainerType);
            var canCreateContainerInstance = Activator.CreateInstance(canCreateContainerType) as ICanCreateContainer;
            var container = canCreateContainerInstance.CreateContainer();
            var configure = With(container, BindingLifecycle.Transient, assembliesConfiguration, assemblyProvider, contractToImplementorsMap);
            configure.EntryAssembly = canCreateContainerType.GetTypeInfo().Assembly;
            configure.Initialize();
            return configure;
        }

        /// <summary>
        /// Configure with a specific <see cref="IContainer"/> and the <see cref="BindingLifecycle">Lifecycle</see> of objects set to none
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to configure with</param>
        /// <param name="assembliesConfiguration"><see cref="AssembliesConfiguration"/> to use</param>
        /// <param name="assemblyProvider"><see cref="IAssemblyProvider"/> to use for providing assemblies</param>
        /// <param name="contractToImplementorsMap"><see cref="IContractToImplementorsMap"/> for keeping track of the relationship between contracts and implementors</param>
        /// <returns>Configuration object to continue configuration on</returns>
        public static Configure With(IContainer container, AssembliesConfiguration assembliesConfiguration, IAssemblyProvider assemblyProvider, IContractToImplementorsMap contractToImplementorsMap)
        {
            return With(container, BindingLifecycle.Transient, assembliesConfiguration, assemblyProvider, contractToImplementorsMap);
        }

        /// <summary>
        /// Configure with a specific <see cref="IContainer"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to configure with</param>
        /// <param name="defaultObjectLifecycle">Default <see cref="BindingLifecycle"/> for object creation/management</param>
        /// <param name="assembliesConfiguration"><see cref="AssembliesConfiguration"/> to use</param>
        /// <param name="assemblyProvider"><see cref="IAssemblyProvider"/> to use for providing assemblies</param>
        /// <param name="contractToImplementorsMap"><see cref="IContractToImplementorsMap"/> for keeping track of the relationship between contracts and implementors</param>
        /// <returns>Configuration object to continue configuration on</returns>
        public static Configure With(IContainer container, BindingLifecycle defaultObjectLifecycle, AssembliesConfiguration assembliesConfiguration, IAssemblyProvider assemblyProvider, IContractToImplementorsMap contractToImplementorsMap)
        {
            return With(container, defaultObjectLifecycle, new DefaultConventions(container), new DefaultBindings(assembliesConfiguration, assemblyProvider, contractToImplementorsMap), assembliesConfiguration);
        }

        /// <summary>
        /// Reset configuration
        /// </summary>
        public static void Reset()
        {
            lock (InstanceLock) Instance = null;
        }


        /// <summary>
        /// Configure with a specific <see cref="IContainer"/>, <see cref="IDefaultConventions"/> and <see cref="IDefaultBindings"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to configure with</param>
        /// <param name="defaultConventions"><see cref="IDefaultConventions"/> to use</param>
        /// <param name="defaultBindings"><see cref="IDefaultBindings"/> to use</param>
        /// <param name="assembliesConfiguration"><see cref="AssembliesConfiguration"/> to use</param>
        /// <returns></returns>
        public static Configure With(IContainer container, IDefaultConventions defaultConventions, IDefaultBindings defaultBindings, AssembliesConfiguration assembliesConfiguration)
        {
            return With(container, BindingLifecycle.Transient, defaultConventions, defaultBindings, assembliesConfiguration);
        }


        /// <summary>
        /// Configure with a specific <see cref="IContainer"/>, <see cref="IDefaultConventions"/> and <see cref="IDefaultBindings"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to configure with</param>
        /// <param name="defaultObjectLifecycle">Default <see cref="BindingLifecycle"/> for object creation/management</param>
        /// <param name="defaultConventions"><see cref="IDefaultConventions"/> to use</param>
        /// <param name="defaultBindings"><see cref="IDefaultBindings"/> to use</param>
        /// <param name="assembliesConfiguration"><see cref="AssembliesConfiguration"/> to use</param>
        /// <returns></returns>
        public static Configure With(IContainer container, BindingLifecycle defaultObjectLifecycle, IDefaultConventions defaultConventions, IDefaultBindings defaultBindings, AssembliesConfiguration assembliesConfiguration)
        {
            if (Instance == null)
            {
                lock (InstanceLock)
                {
                    Instance = new Configure(container, defaultObjectLifecycle, defaultConventions, defaultBindings, assembliesConfiguration);
                }
            }

            return Instance;
        }

#pragma warning disable 1591 // Xml Comments
        public IContainer Container { get; private set; }
        public string SystemName { get; set; }
        public Assembly EntryAssembly { get; private set; }
        public AssembliesConfiguration AssembliesConfiguration { get; private set; }
        public IDefaultStorageConfiguration DefaultStorage { get; set; }
        public ICommandsConfiguration Commands { get; private set; }
        public IEventsConfiguration Events { get; private set; }
        public ITasksConfiguration Tasks { get; private set; }
        public IViewsConfiguration Views { get; private set; }
        public IBindingConventionManager ConventionManager { get; private set; }
        public ISagasConfiguration Sagas { get; private set; }
        public ISerializationConfiguration Serialization { get; private set; }
        public IFrontendConfiguration Frontend { get; private set; }
        public ICallContextConfiguration CallContext { get; private set; }
        public IExecutionContextConfiguration ExecutionContext { get; private set; }
        public ISecurityConfiguration Security { get; private set; }
        public AssembliesConfiguration Assemblies { get; private set; }
        public IQualityAssurance QualityAssurance { get; private set; }
        public CultureInfo Culture { get; set; }
        public CultureInfo UICulture { get; set; }

        public BindingLifecycle DefaultLifecycle 
        {
            get { return Container.DefaultLifecycle; }
            set { Container.DefaultLifecycle = value; }
        }

        public void Initialize()
        {
            ConfigureFromCanConfigurables();
            InitializeCulture();
            
            var initializers = new Action[] {
                () => Serialization.Initialize(Container),
                () => Commands.Initialize(Container),
                () => Events.Initialize(Container),
                () => Tasks.Initialize(Container),
                () => Views.Initialize(Container),
                () => Sagas.Initialize(Container),
                () => Frontend.Initialize(Container),
                () => CallContext.Initialize(Container),
                () => ExecutionContext.Initialize(Container),
                () => Security.Initialize(Container),
                () => DefaultStorage.Initialize(Container)
            };

#if (SILVERLIGHT)
            initializers.ForEach(initializer => initializer());
#else
            Parallel.ForEach(initializers, initializator => initializator());
#endif
            ConfigurationDone();
        }
#pragma warning restore 1591 // Xml Comments


        void InitializeProperties()
        {
            Commands = Container.Get<ICommandsConfiguration>();
            Events = Container.Get<IEventsConfiguration>();
            Tasks = Container.Get<ITasksConfiguration>();
            Views = Container.Get<IViewsConfiguration>();
            ConventionManager = Container.Get<IBindingConventionManager>();
            Sagas = Container.Get<ISagasConfiguration>();
            Serialization = Container.Get<ISerializationConfiguration>();
            DefaultStorage = Container.Get<IDefaultStorageConfiguration>();
            Frontend = Container.Get<IFrontendConfiguration>();
            CallContext = Container.Get<ICallContextConfiguration>();
            ExecutionContext = Container.Get<IExecutionContextConfiguration>();
            Security = Container.Get<ISecurityConfiguration>();
            QualityAssurance = Container.Get<IQualityAssurance>();
        }

        void InitializeCulture()
        {
            if (Culture == null)
                Culture = CultureInfo.InvariantCulture;
            if (UICulture == null)
                UICulture = CultureInfo.InvariantCulture;
        }

        void ConfigureFromCanConfigurables()
        {
            var callbacks = Container.Get<IInstancesOf<ICanConfigure>>();
            callbacks.ForEach(c => c.Configure(this));
        }

        void ConfigurationDone()
        {
            var callbacks = Container.Get<IInstancesOf<IWantToKnowWhenConfigurationIsDone>>();
            callbacks.ForEach(c => c.Configured(this));
        }

        static Type DiscoverCanCreateContainerType(IEnumerable<Assembly> assemblies)
        {
            Type createContainerType = null;
            foreach (var assembly in assemblies.ToArray())
            {
                var type = assembly.DefinedTypes.Select(t => t.AsType()).Where(t => t.HasInterface(typeof(ICanCreateContainer))).SingleOrDefault();
                if (type != null)
                {
                    ThrowIfAmbiguousMatchFoundForCanCreateContainer(createContainerType);

                    createContainerType = type;
                }
            }
            return createContainerType;
        }

        static AssembliesConfigurationBuilder BuildAssembliesConfigurationIfCallbackDefined(Action<AssembliesConfigurationBuilder> assembliesConfigurationBuilderCallback)
        {
            var builder = new AssembliesConfigurationBuilder();
            if (assembliesConfigurationBuilderCallback != null) assembliesConfigurationBuilderCallback(builder);
            if (builder.RuleBuilder == null) builder.IncludeAll();
            return builder;
        }

        
        static void ThrowIfAmbiguousMatchFoundForCanCreateContainer(Type createContainerType)
        {
            if (createContainerType != null)
                throw new AmbiguousContainerCreationException();
        }

        static void ThrowIfCanCreateContainerDoesNotHaveDefaultConstructor(Type createContainerType)
        {
            if (!createContainerType.HasDefaultConstructor())
                throw new MissingDefaultConstructorException(createContainerType);
        }

        static void ThrowIfCanCreateContainerNotFound(Type createContainerType)
        {
            if (createContainerType == null)
                throw new CanCreateContainerNotFoundException();
        }
    }
}
