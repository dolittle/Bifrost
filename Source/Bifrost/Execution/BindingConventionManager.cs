/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IBindingConventionManager"/>
    /// </summary>
    [Singleton]
    public class BindingConventionManager : IBindingConventionManager
    {
        readonly IContainer _container;
        readonly ITypeDiscoverer _typeDiscoverer;
        readonly List<Type> _conventions;

		/// <summary>
		/// Initializes a new instance <see cref="BindingConventionManager"/>
		/// </summary>
		/// <param name="container">The <see cref="IContainer"/> that bindings are resolved to</param>
		/// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to discover binding conventions with</param>
        public BindingConventionManager(IContainer container, ITypeDiscoverer typeDiscoverer)
        {
            _container = container;
            _typeDiscoverer = typeDiscoverer;
            _conventions = new List<Type>();
        }

#pragma warning disable 1591 // Xml Comments
		public void Add(Type type)
        {
            if( !_conventions.Contains(type))
                _conventions.Add(type);
        }

        public void Add<T>() where T : IBindingConvention
        {
            Add(typeof(T));
        }


        public void Initialize()
        {
            var boundServices = _container.GetBoundServices();
            var existingBindings = new Dictionary<Type, Type>();

            foreach (var boundService in boundServices)
                existingBindings[boundService] = boundService;

            var allTypes = _typeDiscoverer.GetAll();
            var services = allTypes.Where(t => !existingBindings.ContainsKey(t)).ToList();

            var resolvedServices = new List<Type>();

            foreach( var conventionType in _conventions )
            {
                var convention = _container.Get(conventionType) as IBindingConvention;
                if( convention != null )
                {
                    var servicesToResolve = services.Where(s => convention.CanResolve(_container, s) && !_container.HasBindingFor(s));

                    foreach (var service in servicesToResolve)
                    {
                        convention.Resolve(_container, service);
                        resolvedServices.Add(service);
                    }
                    resolvedServices.ForEach(t => services.Remove(t));
                }
            }
        }

        public void DiscoverAndInitialize()
        {
            var conventionTypes = _typeDiscoverer.FindMultiple<IBindingConvention>();
            foreach( var conventionType in conventionTypes )
                Add(conventionType);

            Initialize();
		}
#pragma warning restore 1591 // Xml Comments
	}
}