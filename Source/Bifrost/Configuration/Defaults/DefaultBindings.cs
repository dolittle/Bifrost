/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration.Assemblies;
using Bifrost.Execution;

namespace Bifrost.Configuration.Defaults
{
    /// <summary>
    /// Represents a <see cref="IDefaultBindings"/>
    /// </summary>
    public class DefaultBindings : IDefaultBindings
	{
        AssembliesConfiguration _assembliesConfiguration;
        IAssemblyProvider _assemblyProvider;
        IContractToImplementorsMap _contractToImplentorsMap;

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultBindings"/>
        /// </summary>
        public DefaultBindings(AssembliesConfiguration assembliesConfiguration, IAssemblyProvider assemblyProvider, IContractToImplementorsMap contractToImplentorsMap)
        {
            _assembliesConfiguration = assembliesConfiguration;
            _assemblyProvider = assemblyProvider;
            _contractToImplentorsMap = contractToImplentorsMap;
        }

#pragma warning disable 1591 // Xml Comments
		public void Initialize(IContainer container)
        {
            container.Bind(container);
            container.Bind<IContractToImplementorsMap>(_contractToImplentorsMap);
            container.Bind<AssembliesConfiguration>(_assembliesConfiguration);
            container.Bind<IAssemblyProvider>(_assemblyProvider);
            container.Bind<IAssemblies>(typeof(global::Bifrost.Execution.Assemblies), BindingLifecycle.Singleton);
            container.Bind<ITypeDiscoverer>(typeof(TypeDiscoverer), BindingLifecycle.Singleton);
            container.Bind<ITypeFinder>(typeof(TypeFinder), BindingLifecycle.Singleton);
		}
#pragma warning restore 1591 // Xml Comments
	}
}