/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using System.Reflection;

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
        readonly ITypeDiscoverer _typeDiscoverer;
        readonly IContainer _container;
        readonly Dictionary<Type, Type> _migratorTypes;

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

        void Initialize()
        {
            var migratorTypes = _typeDiscoverer.FindMultiple(typeof(IEventMigrator<,>));
            foreach (var migrator in migratorTypes)
            {
                RegisterMigrator(migrator);
            }
        }

        static Type GetSourceType(Type migratorType)
        {
            var @interface = migratorType.GetTypeInfo().ImplementedInterfaces.Where(i => 
                i.GetTypeInfo().IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEventMigrator<,>)
            ).First();

            var type = @interface.GetTypeInfo().GenericTypeArguments.First();
            return type;
        }

    }
}