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
        public FluentConfiguration FluentConfiguration { get; }
        public global::NHibernate.Cfg.Configuration Configuration { get; private set; }

        public EntityContextConnection()
        {
            FluentConfiguration = Fluently.Configure();
        }

        static void DiscoverClassMapsAndAddAssemblies(
            IAssemblies assemblies,
            ITypeDiscoverer typeDiscoverer,
            MappingConfiguration mappings)
        {
            var assembliesWithFluentMappings = typeDiscoverer
                .FindMultiple(typeof(IMappingProvider))
                .Select(t => t.Assembly)
                .Distinct();
            foreach (var assembly in assembliesWithFluentMappings)
            {
                mappings.FluentMappings.AddFromAssembly(assembly).Conventions.Add(DefaultLazy.Never(), AutoImport.Never());
            }

            var assembliesWithHbmMappings = assemblies
                .GetAll()
                .Where(a => a.GetManifestResourceNames().Any(s => s.EndsWith(".hbm.xml")));
            foreach (var assembly in assembliesWithHbmMappings)
            {
                mappings.HbmMappings.AddFromAssembly(assembly);
            }
        }

        public void Initialize(IContainer container)
        {
            var assemblies = container.Get<IAssemblies>();
            var typeDiscoverer = container.Get<ITypeDiscoverer>();
            FluentConfiguration.Mappings(m => DiscoverClassMapsAndAddAssemblies(assemblies, typeDiscoverer, m));
            Configuration = FluentConfiguration.BuildConfiguration();
            SessionFactory = Configuration.BuildSessionFactory();
        }
    }
}