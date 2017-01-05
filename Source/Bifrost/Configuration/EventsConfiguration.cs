/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Events;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventsConfiguration"/>
    /// </summary>
    public class EventsConfiguration : ConfigurationStorageElement, IEventsConfiguration
    {
        IEventStoreChangeManager _eventStoreChangeManager;

        /// <summary>
        /// Initializes an instance of <see cref="EventsConfiguration"/>
        /// </summary>
        /// <param name="eventStoreChangeManager">An instance of <see cref="IEventStoreChangeManager"/></param>
        public EventsConfiguration(IEventStoreChangeManager eventStoreChangeManager)
        {
            _eventStoreChangeManager = eventStoreChangeManager;
            EventStoreType = typeof(NullEventStore);
            EventSubscriptionsType = typeof(NullEventSubscriptions);
            UncommittedEventStreamCoordinatorType = typeof(NullUncommittedEventStreamCoordinator);
        }

#pragma warning disable 1591 // Xml Comments
        public Type EventStoreType { get; set; }
        public Type EventSubscriptionsType { get; set; }
        public Type UncommittedEventStreamCoordinatorType { get; set; }

        public void AddEventStoreChangeNotifier(Type type)
        {
            _eventStoreChangeManager.Register(type);
        }

        public override void Initialize(IContainer container)
        {
            container.Bind<IUncommittedEventStreamCoordinator>(UncommittedEventStreamCoordinatorType);

            container.Bind<IEventStore>(EventStoreType, BindingLifecycle.Singleton);

            container.Bind<IEventSubscriptions>(EventSubscriptionsType, BindingLifecycle.Singleton);

            if (EntityContextConfiguration != null)
            {
                EntityContextConfiguration.BindEntityContextTo<IEvent>(container);
                EntityContextConfiguration.BindEntityContextTo<EventSubscription>(container);
                base.Initialize(container);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}