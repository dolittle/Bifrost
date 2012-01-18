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
using System.Reflection;
using Bifrost.Configuration.Defaults;
using Bifrost.Execution;
using Microsoft.Practices.ServiceLocation;
using Bifrost.Events;

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

        IConfigurationSource _configurationSource;

        Configure(IContainer container, BindingLifecycle defaultObjectLifecycle,  IDefaultConventions defaultConventions, IDefaultBindings defaultBindings)
        {
            DefaultObjectLifecycle = defaultObjectLifecycle;

            container.Bind<IConfigure>(this);

            Container = container;
            ExcludeNamespacesForTypeDiscovery();
            SetupServiceLocator();

            defaultBindings.Initialize(container);
            defaultConventions.Initialize();

            InitializeProperties();
        }

        /// <summary>
        /// Configure with a specific <see cref="IContainer"/> and the <see cref="BindingLifecycle">Lifecycle</see> of objects set to none
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to configure with</param>
        /// <returns>Configuration object to continue configuration on</returns>
        public static Configure With(IContainer container)
        {
            return With(container, BindingLifecycle.None);
        }

        /// <summary>
        /// Configure with a specific <see cref="IContainer"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to configure with</param>
        /// <param name="defaultObjectLifecycle">Default <see cref="BindingLifecycle"/> for object creation/management</param>
        /// <returns>Configuration object to continue configuration on</returns>
        public static Configure With(IContainer container, BindingLifecycle defaultObjectLifecycle)
        {
            return With(container, defaultObjectLifecycle, new DefaultConventions(), new DefaultBindings());
        }

        /// <summary>
        /// Reset configuration
        /// </summary>
        public static void Reset()
        {
            lock (InstanceLock)
            {
                Instance = null;
            }
        }


        /// <summary>
        /// Configure with a specific <see cref="IContainer"/>, <see cref="IDefaultConventions"/> and <see cref="IDefaultBindings"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to configure with</param>
        /// <param name="defaultConventions"><see cref="IDefaultConventions"/> to use</param>
        /// <param name="defaultBindings"><see cref="IDefaultBindings"/> to use</param>
        /// <returns></returns>
        public static Configure With(IContainer container, IDefaultConventions defaultConventions, IDefaultBindings defaultBindings)
        {
            return With(container, BindingLifecycle.None, defaultConventions, defaultBindings);
        }


        /// <summary>
        /// Configure with a specific <see cref="IContainer"/>, <see cref="IDefaultConventions"/> and <see cref="IDefaultBindings"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to configure with</param>
        /// <param name="defaultObjectLifecycle">Default <see cref="BindingLifecycle"/> for object creation/management</param>
        /// <param name="defaultConventions"><see cref="IDefaultConventions"/> to use</param>
        /// <param name="defaultBindings"><see cref="IDefaultBindings"/> to use</param>
        /// <returns></returns>
        public static Configure With(IContainer container, BindingLifecycle defaultObjectLifecycle, IDefaultConventions defaultConventions, IDefaultBindings defaultBindings)
        {
            if (Instance == null)
            {
                lock (InstanceLock)
                {
                    Instance = new Configure(container, defaultObjectLifecycle, defaultConventions, defaultBindings);
                }
            }

            return Instance;
        }

#pragma warning disable 1591 // Xml Comments
        public Type LoggerType { get; set; }
        public IContainer Container { get; private set; }
        public ICommandsConfiguration Commands { get; private set; }
        public IEventsConfiguration Events { get; private set; }
        public IViewsConfiguration Views { get; private set; }
        public IBindingConventionManager ConventionManager { get; private set; }
        public IApplicationManager ApplicationManager { get; private set; }
        public IApplication Application { get; private set; }
		public ISagasConfiguration Sagas { get; private set; }
		public CultureInfo Culture { get; set; }
		public CultureInfo UICulture { get; set; }
        public BindingLifecycle DefaultObjectLifecycle { get; set; }

		public void ConfigurationSource(IConfigurationSource configurationSource)
        {
            _configurationSource = configurationSource;
        }

        public void Initialize()
        {
            if (_configurationSource != null)
                _configurationSource.Initialize(this);

            Events.Initialize(this);
            Views.Initialize(this);
			Sagas.Initialize(this);
            InitializeApplication();
        	InitializeCulture();
        }
#pragma warning restore 1591 // Xml Comments

        void SetupServiceLocator()
        {
            var serviceLocator = new ContainerServiceLocator(Container);
            Container.Bind<IServiceLocator>(serviceLocator);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        void InitializeProperties()
        {
            Commands = Container.Get<ICommandsConfiguration>();
            Events = Container.Get<IEventsConfiguration>();
            Views = Container.Get<IViewsConfiguration>();
            ConventionManager = Container.Get<IBindingConventionManager>();
        	Sagas = Container.Get<ISagasConfiguration>();
            ApplicationManager = Container.Get<IApplicationManager>();
        }

		void InitializeCulture()
		{
			if (Culture == null)
				Culture = CultureInfo.InvariantCulture;
			if (UICulture == null)
				UICulture = CultureInfo.InvariantCulture;
		}

        private void InitializeApplication()
        {
            Application = ApplicationManager.Get();
        }

        static void ExcludeNamespacesForTypeDiscovery()
        {
            // TODO : This is BAD..  Need to move this away, maybe include a list of files some how for all extensions instead!!!!
            // reason its bad, Core suddenly knows about all extensions and what not.   No Good!
            TypeDiscoverer.ExcludeNamespaceStartingWith("NHibernate");
            TypeDiscoverer.ExcludeNamespaceStartingWith("FluentNHibernate");
            TypeDiscoverer.ExcludeNamespaceStartingWith("Castle");
            TypeDiscoverer.ExcludeNamespaceStartingWith("log4net");
            TypeDiscoverer.ExcludeNamespaceStartingWith("Iesi");
            TypeDiscoverer.ExcludeNamespaceStartingWith("Ninject");
            TypeDiscoverer.ExcludeNamespaceStartingWith("Microsoft");
            TypeDiscoverer.ExcludeNamespaceStartingWith("AutoMapper");
            TypeDiscoverer.ExcludeNamespaceStartingWith("NServiceBus");
        }
    }
}
