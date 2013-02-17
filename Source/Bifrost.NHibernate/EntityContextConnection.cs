#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
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
                mappings.FluentMappings.AddFromAssembly(assembly).Conventions.Add(DefaultLazy.Never());
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