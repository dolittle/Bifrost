/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Configuration;
using Bifrost.Events;
using Bifrost.Events.InProcess;
using Bifrost.Execution;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventsConfiguration"/>
    /// </summary>
    public class EventsConfiguration : ConfigurationStorageElement, IEventsConfiguration
    {
        /// <summary>
        /// Initializes an instance of <see cref="EventsConfiguration"/>
        /// </summary>
        public EventsConfiguration()
        {
            EventStore = typeof(NullEventStore);
            EventSubscriptions = typeof(NullEventSubscriptions);
            UncommittedEventStreamCoordinator = typeof(NullUncommittedEventStreamCoordinator);
            CommittedEventStreamSender = typeof(NullCommittedEventStreamSender);
            CommittedEventStreamReceiver = typeof(NullCommittedEventStreamReceiver);
        }

        /// <inheritdoc/>
        public Type EventStore { get; set; }

        /// <inheritdoc/>
        public Type EventSubscriptions { get; set; }

        /// <inheritdoc/>
        public Type UncommittedEventStreamCoordinator { get; set; }

        /// <inheritdoc/>
        public Type CommittedEventStreamSender { get; set; }

        /// <inheritdoc/>
        public Type CommittedEventStreamReceiver { get; set; }

        /// <inheritdoc/>
        public override void Initialize(IContainer container)
        {
            container.Bind<IUncommittedEventStreamCoordinator>(UncommittedEventStreamCoordinator);
            container.Bind<ICanSendCommittedEventStream>(CommittedEventStreamSender, BindingLifecycle.Singleton);
            container.Bind<ICanReceiveCommittedEventStream>(CommittedEventStreamReceiver, BindingLifecycle.Singleton);
            container.Bind<IEventStore>(EventStore, BindingLifecycle.Singleton);
            container.Bind<IEventSubscriptions>(EventSubscriptions, BindingLifecycle.Singleton);

            if (EntityContextConfiguration != null)
            {
                EntityContextConfiguration.BindEntityContextTo<IEvent>(container);
                EntityContextConfiguration.BindEntityContextTo<EventSubscription>(container);
                base.Initialize(container);
            }
        }
    }
}