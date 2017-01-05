/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Configuration.Defaults
{
	/// <summary>
	/// Represents a <see cref="IDefaultConventions"/> implementation
	/// </summary>
    public class DefaultConventions : IDefaultConventions
	{
		IContainer _container;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Bifrost.Configuration.Defaults.DefaultConventions"/> class.
		/// </summary>
		public DefaultConventions(IContainer container)
		{
			_container = container;
			_container.Bind<IBindingConventionManager>(typeof(BindingConventionManager));
		}
		
#pragma warning disable 1591 // Xml Comments
		public void Initialize()
        {
            var conventionManager = _container.Get<IBindingConventionManager>();
            conventionManager.Add<DefaultConvention>();
            conventionManager.DiscoverAndInitialize();
		}
#pragma warning restore 1591 // Xml Comments
	}
}