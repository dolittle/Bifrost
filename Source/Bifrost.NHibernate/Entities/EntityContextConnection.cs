/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Bifrost.Entities;
using Bifrost.Execution;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;

namespace Bifrost.NHibernate.Entities
{
	public class EntityContextConnection : IEntityContextConnection, IConnection
	{
		public ISessionFactory SessionFactory { get; private set; }
		public FluentConfiguration FluentConfiguration { get; private set; }
		public global::NHibernate.Cfg.Configuration Configuration { get; private set; }

        public EntityContextConnection()
        {
            FluentConfiguration = Fluently.Configure();
        }

        void DiscoverClassMapsAndAddAssemblies(ITypeDiscoverer typeDiscoverer, MappingConfiguration mappings)
        {
            var assemblies = typeDiscoverer.FindMultiple(typeof(IMappingProvider)).Select(t => t.Assembly).Distinct();
            foreach (var assembly in assemblies)
            {
                mappings.FluentMappings.AddFromAssembly(assembly).Conventions.Add(DefaultLazy.Never(), AutoImport.Never());
                mappings.HbmMappings.AddFromAssembly(assembly);
            }
                
        }

        public void Initialize(IContainer container)
        {
            var typeDiscoverer = container.Get<ITypeDiscoverer>();
            FluentConfiguration.Mappings(m => DiscoverClassMapsAndAddAssemblies(typeDiscoverer, m));
            Configuration = FluentConfiguration.BuildConfiguration();
            SessionFactory = Configuration.BuildSessionFactory();
        }
    }
}