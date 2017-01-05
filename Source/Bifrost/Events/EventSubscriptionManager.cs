/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Globalization;
using System.Reflection;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSubscriptionManager"/>
    /// </summary>
    [Singleton]
    public class EventSubscriptionManager : IEventSubscriptionManager
    {
        IEventSubscriptions _subscriptions;
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        ILocalizer _localizer;
        IEnumerable<EventSubscription> _subscriptionsFromRepository;
        IEnumerable<EventSubscription> _subscriptionsInProcess;
        List<EventSubscription> _allSubscriptions = new List<EventSubscription>();

        /// <summary>
        /// Initializes an instance of <see cref="EventSubscriptionManager"/>
        /// </summary>
        /// <param name="subscriptions">A <see cref="IEventSubscriptions"/> that will be used to maintain subscriptions from a datasource</param>
        /// <param name="typeDiscoverer">A <see cref="ITypeDiscoverer"/> for discovering <see cref="IProcessEvents"/>s in current process</param>
        /// <param name="container">A <see cref="IContainer"/> for creating instances of objects/services</param>
        /// <param name="localizer">A <see cref="ILocalizer"/> for controlling localization while executing subscriptions</param>
        public EventSubscriptionManager(
            IEventSubscriptions subscriptions,
            ITypeDiscoverer typeDiscoverer, 
            IContainer container,
            ILocalizer localizer)
        {
            _subscriptions = subscriptions;
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _localizer = localizer;

            RefreshAndMergeSubscriptionsFromRepository();
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<EventSubscription> GetAllSubscriptions()
        {
            return _allSubscriptions;
        }

        public IEnumerable<EventSubscription> GetAvailableSubscriptions()
        {
            var availableSubscriptions = _allSubscriptions.Where(s => _subscriptionsInProcess.Contains(s));
            return availableSubscriptions;
        }

        public void Process(EventSubscription subscription, IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
                Process(subscription, @event);
        }

        public void Process(EventSubscription subscription, IEvent @event)
        {
            var subscriber = _container.Get(subscription.Owner) as IProcessEvents;
            Process(subscription, subscriber, @event);
        }


        public void Process(IEnumerable<IEvent> events)
        {
            RefreshAndMergeSubscriptionsFromRepository();
            var subscribers = GetSubscriberInstancesFromEvents(events);

            foreach (var @event in events)
                Process(subscribers, @event);
        }

        public void Process(IEvent @event)
        {
            RefreshAndMergeSubscriptionsFromRepository();
            var subscribers = GetSubscriberInstancesFromEvents(new[] { @event });
            Process(subscribers, @event);
        }

#pragma warning restore 1591 // Xml Comments

        Dictionary<Type, IProcessEvents> GetSubscriberInstancesFromEvents(IEnumerable<IEvent> events)
        {
            var subscribersBySubscriberTypes = new Dictionary<Type, IProcessEvents>();

            foreach (var @event in events)
            {
                var eventType = @event.GetType();
                var subscriptions = _allSubscriptions.Where(s => s.EventType.Equals(eventType));
                foreach (var subscription in subscriptions)
                {
                    IProcessEvents instance = null;
                    if (subscribersBySubscriberTypes.ContainsKey(subscription.Owner))
                        instance = subscribersBySubscriberTypes[subscription.Owner];
                    else
                    {
                        instance = _container.Get(subscription.Owner) as IProcessEvents;
                        subscribersBySubscriberTypes[subscription.Owner] = instance;
                    }
                }
            }

            return subscribersBySubscriberTypes;
        }


        void Process(Dictionary<Type, IProcessEvents> subscribers, IEvent @event)
        {
            var eventType = @event.GetType();
            var subscriptionsToProcess = _allSubscriptions.Where(s => s.EventType.Equals(eventType));
            foreach (var subscriptionToProcess in subscriptionsToProcess)
            {
                if (!subscriptionToProcess.ShouldProcess(@event))
                    continue;

                Process(subscriptionToProcess, subscribers[subscriptionToProcess.Owner], @event);
            }
        }

        void Process(EventSubscription subscription, IProcessEvents subscriber, IEvent @event)
        {
            using (_localizer.BeginScope())
            {
                subscription.Method.Invoke(subscriber, new[] { @event });
                UpdateExistingSubscriptionFrom(subscription, @event.Id);
            }
        }

        void UpdateExistingSubscriptionFrom(EventSubscription subscription, long eventId)
        {
            var subscriptionToUpdate = _allSubscriptions.Where(e=>e.Equals(subscription)).Single();
            subscriptionToUpdate.LastEventId = eventId;
            _subscriptions.Save(subscriptionToUpdate);
        }


        void CollectInProcessSubscribers()
        {
            var subscriptionsInProcess = new List<EventSubscription>();
            var eventSubscriberTypes = _typeDiscoverer.FindMultiple<IProcessEvents>();

            foreach (var eventSubscriberType in eventSubscriberTypes)
            {
                var subscriptions = (from m in 
                                       eventSubscriberType.GetTypeInfo().DeclaredMethods
                                  where m.Name == ProcessMethodInvoker.ProcessMethodName &&
                                        m.GetParameters().Length == 1 &&
                                        typeof(IEvent)
                                            .GetTypeInfo().IsAssignableFrom(m.GetParameters()[0].ParameterType.GetTypeInfo())
                                  select new EventSubscription
                                  {
                                      Id = Guid.Empty,
                                      Owner = eventSubscriberType,
                                      Method = m,
                                      EventType = m.GetParameters()[0].ParameterType,
                                      EventName = m.GetParameters()[0].ParameterType.Name,
                                      LastEventId = 0
                                  }).ToArray();

                subscriptionsInProcess.AddRange(subscriptions);
            }
            _subscriptionsInProcess = subscriptionsInProcess;
        }


        void RefreshAndMergeSubscriptionsFromRepository()
        {
            _allSubscriptions.Clear();
            CollectInProcessSubscribers();
            _allSubscriptions.Clear();
            _subscriptionsFromRepository = _subscriptions.GetAll();
            AddSubscriptionsFromRepository();
            AddInMemoryOrUseRepositorySubscriptions();
            UpdateInMemorySubscriptions();
            RegisterSubscriptionsThatIsOnlyInMemory();
        }

        void RegisterSubscriptionsThatIsOnlyInMemory()
        {
            var subscribersNotInRepository = _subscriptionsInProcess.Where(s => !_subscriptionsFromRepository.Contains(s));
            foreach (var subscriber in subscribersNotInRepository)
            {
                subscriber.Id = Guid.NewGuid();
                _subscriptions.Save(subscriber);
            }
        }

        void AddInMemoryOrUseRepositorySubscriptions()
        {
            foreach (var subscription in _subscriptionsInProcess)
            {
                if (_subscriptionsFromRepository.Contains(subscription))
                {
                    var existing = _subscriptionsFromRepository.Where(s => s.Equals(subscription)).Single();
                    _allSubscriptions.Add(existing);
                }
                else
                    _allSubscriptions.Add(subscription);
            }
        }

        void AddSubscriptionsFromRepository()
        {
            var subscribersNotInProcess = _subscriptionsFromRepository.Where(s => !_subscriptionsInProcess.Contains(s));
            _allSubscriptions.AddRange(subscribersNotInProcess);
        }

        void UpdateInMemorySubscriptions()
        {
            var subscribersToUpdate = _subscriptionsFromRepository.Where(s => _subscriptionsInProcess.Where(ss => ss.Equals(s)).Count() == 1);
            foreach (var subscriber in subscribersToUpdate)
            {
                var subscriberToUpdate = _subscriptionsInProcess.Where(s => s.Equals(subscriber)).Single();
                if (subscriber.LastEventId > subscriberToUpdate.LastEventId)
                    subscriberToUpdate.LastEventId = subscriber.LastEventId;
            }
        }
    }
}
