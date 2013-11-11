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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

#if(SILVERLIGHT)
using System.Windows;
#endif

#if(NETFX_CORE)
using Windows.Storage;
#endif
using System.Threading.Tasks;
using Bifrost.Configuration.Defaults;
using Bifrost.Execution;
using Bifrost.Extensions;
using Bifrost.Diagnostics;


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


        Configure(IContainer container, BindingLifecycle defaultLifecycle,  IDefaultConventions defaultConventions, IDefaultBindings defaultBindings)
        {
            SystemName = "[Not Set]";

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
        public static Configure DiscoverAndConfigure()
        {
            var assemblies = GetAssembliesCurrentlyInMemory();
            var canCreateContainerType = DiscoverCanCreateContainerType(assemblies);
            ThrowIfCanCreateContainerNotFound(canCreateContainerType);
            ThrowIfCanCreateContainerDoesNotHaveDefaultConstructor(canCreateContainerType);
            var canCreateContainerInstance = Activator.CreateInstance(canCreateContainerType) as ICanCreateContainer;
            var container = canCreateContainerInstance.CreateContainer();
            var configure = With(container, BindingLifecycle.Transient);
            configure.EntryAssembly = canCreateContainerType.Assembly;
            configure.Initialize();
            return configure;
        }

        /// <summary>
        /// Configure with a specific <see cref="IContainer"/> and the <see cref="BindingLifecycle">Lifecycle</see> of objects set to none
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to configure with</param>
        /// <returns>Configuration object to continue configuration on</returns>
        public static Configure With(IContainer container)
        {
            return With(container, BindingLifecycle.Transient);
        }

        /// <summary>
        /// Configure with a specific <see cref="IContainer"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> to configure with</param>
        /// <param name="defaultObjectLifecycle">Default <see cref="BindingLifecycle"/> for object creation/management</param>
        /// <returns>Configuration object to continue configuration on</returns>
        public static Configure With(IContainer container, BindingLifecycle defaultObjectLifecycle)
        {
            return With(container, defaultObjectLifecycle, new DefaultConventions(container), new DefaultBindings());
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
        /// <returns></returns>
        public static Configure With(IContainer container, IDefaultConventions defaultConventions, IDefaultBindings defaultBindings)
        {
            return With(container, BindingLifecycle.Transient, defaultConventions, defaultBindings);
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
        public IContainer Container { get; private set; }
        public string SystemName { get; set; }
        public Assembly EntryAssembly { get; private set; }
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
            
            var initializationList = new List<Action>();
            initializationList.Add(() => Serialization.Initialize(Container));
            initializationList.Add(() => Commands.Initialize(Container));
            initializationList.Add(() => Events.Initialize(Container));
            initializationList.Add(() => Tasks.Initialize(Container));
            initializationList.Add(() => Views.Initialize(Container));
            initializationList.Add(() => Sagas.Initialize(Container));
            initializationList.Add(() => Frontend.Initialize(Container));
            initializationList.Add(() => CallContext.Initialize(Container));
            initializationList.Add(() => ExecutionContext.Initialize(Container));
            initializationList.Add(() => Security.Initialize(Container));
            initializationList.Add(() => DefaultStorage.Initialize(Container));

            #if(SILVERLIGHT)
            initializationList.ForEach(initializator => initializator());
            #else
            Parallel.ForEach(initializationList, initializator => initializator());
            #endif
            
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
            var typeImporter = Container.Get<ITypeImporter>();
            foreach (var canConfigure in typeImporter.ImportMany<ICanConfigure>())
            {
                canConfigure.Configure(this);
            }
        }
        
        static Type DiscoverCanCreateContainerType(IEnumerable<Assembly> assemblies)
        {
            Type createContainerType = null;
            foreach (var assembly in assemblies)
            {
#if(NETFX_CORE)
                var type = assembly.DefinedTypes.Select(t => t.AsType()).Where(t => t.HasInterface(typeof(ICanCreateContainer))).SingleOrDefault();
#else
                var type = assembly.GetTypes().Where(t => t.HasInterface(typeof(ICanCreateContainer))).SingleOrDefault();
#endif
                if (type != null)
                {
                    ThrowIfAmbiguousMatchFoundForCanCreateContainer(createContainerType);

                    createContainerType = type;
                }
            }
            return createContainerType;
        }


        static IEnumerable<Assembly> GetAssembliesCurrentlyInMemory()
        {
#if(SILVERLIGHT)
            var assemblies = (from part in Deployment.Current.Parts
                          where ShouldAddAssembly(part.Source)
                          let info = Application.GetResourceStream(new Uri(part.Source, UriKind.Relative))
                          select part.Load(info.Stream)).ToArray();
#else 
#if(NETFX_CORE)
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var assembliesLoaded = new List<Assembly>();

            IEnumerable<StorageFile>    files = null;

            var operation = folder.GetFilesAsync();
            operation.Completed = async (r, s) => {
                var result = await r;
                files = result;
            };

            while (files == null) ;

            foreach (var file in files)
            {
                if (file.FileType == ".dll" || file.FileType == ".exe")
                {
                    var name = new AssemblyName() { Name = System.IO.Path.GetFileNameWithoutExtension(file.Name) };
                    try
                    {
                        Assembly asm = Assembly.Load(name);
                        assembliesLoaded.Add(asm);
                    }
                    catch { }
                }
            }
            var assemblies = assembliesLoaded.ToArray();
#else

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(f =>
            {
                var name = f.GetName().Name;
                return ShouldAddAssembly(name);
            }).ToArray();
#endif
#endif
            return assemblies;
        }

        static bool ShouldAddAssembly(string name)
        {
            return !name.StartsWith("System") && !name.StartsWith("Microsoft");
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
