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

using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Microsoft.Practices.ServiceLocation;

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
        readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<Type, Type> _migratorTypes;

        /// <summary>
        /// Initializes an instance of <see cref="EventMigratorManager">EventMigratorManager</see>
        /// </summary>
        /// <param name="typeDiscoverer"></param>
        /// <param name="serviceLocator"></param>
        public EventMigratorManager(ITypeDiscoverer typeDiscoverer, IServiceLocator serviceLocator)
        {
            _typeDiscoverer = typeDiscoverer;
            _serviceLocator = serviceLocator;
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
                var migrator = (dynamic) _serviceLocator.GetInstance(migratorType);
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
            var types = from interfaceType in migratorType.GetInterfaces()
                         where interfaceType.IsGenericType
                         let baseInterface = interfaceType.GetGenericTypeDefinition()
                         where baseInterface == typeof(IEventMigrator<,>)
                         select interfaceType.GetGenericArguments().First();

            return types.First();
        }

    }
}