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
    /// Represents a <see cref="IEventMigrationHierarchyDiscoverer">IEventMigrationHierarchyDiscoverer</see>
    /// </summary>
    /// <remarks>
    /// The discoverer will automatically build an <see cref="EventMigrationHierarchy">EventMigrationHierarchy</see> for all events.
    /// </remarks>
    [Singleton]
    public class EventMigrationHierarchyDiscoverer : IEventMigrationHierarchyDiscoverer
    {
        private readonly ITypeDiscoverer _typeDiscoverer;
        private static readonly Type _migrationInterface = typeof (IAmNextGenerationOf<>);

        /// <summary>
        /// Initializes an instance of <see cref="EventMigrationHierarchyDiscoverer"/>
        /// </summary>
        /// <param name="typeDiscoverer"></param>
        public EventMigrationHierarchyDiscoverer(ITypeDiscoverer typeDiscoverer)
        {
            _typeDiscoverer = typeDiscoverer;
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<EventMigrationHierarchy> GetMigrationHierarchies()
        {
            var allEvents = GetAllEventTypes();
            var logicalEvents = GetLogicalEvents(allEvents);
            return logicalEvents.Select(logicalEvent => GetMigrationHierarchy(logicalEvent,allEvents)).ToList();
        }
#pragma warning restore 1591 // Xml Comments

        private static EventMigrationHierarchy GetMigrationHierarchy(Type logicalEvent, IEnumerable<Type> allEvents)
        {
            var migrationHierarchy = new EventMigrationHierarchy(logicalEvent);
            var migratedEvents = GetMigratedEvents(allEvents);

            var migrationType = GetMigrationTypeFor(logicalEvent, migratedEvents);
            while (migrationType != null)
            {
                migrationHierarchy.AddMigrationLevel(migrationType);
                migrationType = GetMigrationTypeFor(migrationType, migratedEvents);
            }
            return migrationHierarchy;
        }

        private static Type GetMigrationTypeFor(Type migrationSourceType, IEnumerable<Type> migratedEventTypes)
        {
            return migratedEventTypes.Select(candidateType => GetMigrationType(migrationSourceType, candidateType)).FirstOrDefault(type => type != null);
        }

        private static Type GetMigrationType(Type migrationSourceType, Type candidateType)
        {
            var types = from interfaceType in 
                            candidateType.GetTypeInfo().ImplementedInterfaces
                        where interfaceType.GetTypeInfo().IsGenericType

                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == _migrationInterface && interfaceType
                            .GetTypeInfo().GenericTypeArguments
                            .First() == migrationSourceType
                        select interfaceType
                            .GetTypeInfo().GenericTypeArguments
                            .First();

            var migratedFromType = types.FirstOrDefault();

            return migratedFromType == null ? null : candidateType;
        }

        private static IEnumerable<Type> GetLogicalEvents(IEnumerable<Type> allEventTypes)
        {
            var migratedEvents = GetMigratedEvents(allEventTypes);

            return allEventTypes.Except(migratedEvents);
        }

        private static IEnumerable<Type> GetMigratedEvents(IEnumerable<Type> allEventTypes)
        {
            foreach(var @event in allEventTypes)
            {
                var eventType = (from ievent in @event
                                    .GetTypeInfo().ImplementedInterfaces
                                 where ievent
                                    .GetTypeInfo().IsGenericType
                                 let baseInterface = ievent.GetGenericTypeDefinition()
                                 where baseInterface == _migrationInterface
                                 select ievent).FirstOrDefault();

                if (eventType != null)
                    yield return @event;
            }
        }

        private IEnumerable<Type> GetAllEventTypes()
        {
            return _typeDiscoverer.FindMultiple<IEvent>();
        }
    }
}