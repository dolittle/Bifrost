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

using Bifrost.Configuration;
using Bifrost.Configuration.Xml;
using Bifrost.Execution;
using FluentNHibernate.Cfg.Db;

namespace Bifrost.NHibernate.Configuration
{
    [ElementName("NHibernate")]
    public class NHibernateStorageElement : StorageElement
    {
        readonly IAssemblyLocator _assemblyLocator;
        readonly ITypeDiscoverer _typeDiscoverer;

        public NHibernateStorageElement()
            : this(Configure.Instance.Container.Get<IAssemblyLocator>(),
                Configure.Instance.Container.Get<ITypeDiscoverer>()) { }

        public NHibernateStorageElement(IAssemblyLocator assemblyLocator, ITypeDiscoverer typeDiscoverer)
        {
            _assemblyLocator = assemblyLocator;
            _typeDiscoverer = typeDiscoverer;
            EntityContextType = typeof(EntityContext<>);
        }

        public string ConnectionString { get; set; }

        public override IEntityContextConfiguration GetConfiguration()
        {
            var configuration = new EntityContextConfiguration();
            var connection = new EntityContextConnection(_typeDiscoverer);
            connection.FluentConfiguration.Database(
                MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString));

            configuration.Connection = connection;
            connection.Configure();
            return configuration;
        }
    }
}
