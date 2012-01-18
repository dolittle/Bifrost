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
using System.Linq;
using Bifrost.Entities;
using Bifrost.Execution;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;

namespace Bifrost.NHibernate
{
	public class EntityContextConnection : IEntityContextConnection
	{
        ITypeDiscoverer _typeDiscoverer;

		public ISessionFactory SessionFactory { get; private set; }
		public FluentConfiguration FluentConfiguration { get; private set; }
		public global::NHibernate.Cfg.Configuration Configuration { get; private set; }

		public EntityContextConnection(ITypeDiscoverer typeDiscoverer)
		{
            _typeDiscoverer = typeDiscoverer;
			FluentConfiguration = Fluently.Configure().
				Mappings(m => DiscoverClassMapsAndAddAssemblies(m));

		}

		public void Configure()
		{
			Configuration = FluentConfiguration.BuildConfiguration();
			SessionFactory = Configuration.BuildSessionFactory();
		}

        void DiscoverClassMapsAndAddAssemblies(MappingConfiguration mappings)
        {
            var assemblies = _typeDiscoverer.FindMultiple(typeof(IMappingProvider)).Select(t => t.Assembly).Distinct();
            foreach (var assembly in assemblies)
                mappings.FluentMappings.AddFromAssembly(assembly).Conventions.Add(DefaultLazy.Never());
        }
	}
}