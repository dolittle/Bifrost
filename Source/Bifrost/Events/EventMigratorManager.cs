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
using System.Linq;
using Bifrost.Execution;
#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a <see cref="IEventMigratorManager">IEventMigratorManager</see>
    /// </summary>
    /// <remarks>
    /// The manager will automatically import any <see cref="IEventMigrator{TS,TD}">IEventMigrator</see>
    /// and use them when migrating
    /// </remarks>
    [Singleton]
    public class EventMigratorManager : IEventMigratorManager
    {
        private readonly ITypeDiscoverer _typeDiscoverer;
        readonly IContainer _container;
        private readonly Dictionary<Type, Type> _migratorTypes;

        /// <summary>
        /// Initializes an instance of <see cref="EventMigratorManager">EventMigratorManager</see>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to use for discovering <see cref="IEventMigrator">Event migrators</see></param>
        /// <param name="container"><see cref="IContainer"/> to use for instantiation of <see cref="IEventMigrator">Event migrators</see></param>
        public EventMigratorManager(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _migratorTypes = new Dictionary<Type, Type>();
            Initialize();
        }

#pragma warning disable 1591 // Xml Comments
        public IEvent Migrate(IEvent @event)
        {
            var result = @event;
            Type migratorType;
            while (_migratorTypes.TryGetValue(result.GetType(), out migratorType))
            {
                var migrator = (dynamic) _container.Get(migratorType);
                result = (IEvent) migrator.Migrate((dynamic)result);
            }
            return result;
        }
#pragma warning restore 1591 // Xml Comments


        /// <summary>
        /// Register a migrator
        /// </summary>
        /// <param name="migratorType">Type of migrator to register</param>
        public void RegisterMigrator(Type migratorType)
        {
            // Todo : Validate migrator type!
            var sourceType = GetSourceType(migratorType);
            _migratorTypes.Add(sourceType, migratorType);
        }

        private void Initialize()
        {
            var migratorTypes = _typeDiscoverer.FindMultiple(typeof(IEventMigrator<,>));
            foreach (var migrator in migratorTypes)
            {
                RegisterMigrator(migrator);
            }
        }

        private static Type GetSourceType(Type migratorType)
        {
            var types = from interfaceType in migratorType
#if(NETFX_CORE)
                                    .GetTypeInfo().ImplementedInterfaces
#else
                                    .GetInterfaces()
#endif
                         where interfaceType
#if(NETFX_CORE)
                                    .GetTypeInfo().IsGenericType
#else
                                    .IsGenericType
#endif
                         let baseInterface = interfaceType.GetGenericTypeDefinition()
                         where baseInterface == typeof(IEventMigrator<,>)
                         select interfaceType
#if(NETFX_CORE)
                            .GetTypeInfo().GenericTypeParameters
#else
                            .GetGenericArguments()
#endif
                            .First();

            return types.First();
        }

    }
}