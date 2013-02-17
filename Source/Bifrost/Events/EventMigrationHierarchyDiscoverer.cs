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
#if(NETFX_CORE)
                            candidateType.GetTypeInfo().ImplementedInterfaces
                        where interfaceType.GetTypeInfo().IsGenericType
#else
                            candidateType.GetInterfaces()
                        where interfaceType.IsGenericType
#endif
                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == _migrationInterface && interfaceType
#if(NETFX_CORE)
                            .GetTypeInfo().GenericTypeArguments
#else
                            .GetGenericArguments()
#endif
                            .First() == migrationSourceType
                        select interfaceType
#if(NETFX_CORE)
                            .GetTypeInfo().GenericTypeArguments
#else
                            .GetGenericArguments()
#endif
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
#if(NETFX_CORE)
                                    .GetTypeInfo().ImplementedInterfaces
#else
                                    .GetInterfaces()
#endif
                                 where ievent
#if(NETFX_CORE)
                                    .GetTypeInfo().IsGenericType
#else
                                    .IsGenericType
#endif
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