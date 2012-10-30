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
using Bifrost.Time;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSubscriptionManager"/>
    /// </summary>
    public class EventSubscriptionManager : IEventSubscriptionManager
    {
        List<EventSubscription> _allSubscriptions;
        List<EventSubscription> _availableSubscriptions;
        IEventSubscriptionRepository _repository;
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;

        /// <summary>
        /// Initializes an instance of <see cref="EventSubscriptionManager"/>
        /// </summary>
        /// <param name="repository">A <see cref="IEventSubscriptionRepository"/> that will be used to maintain subscriptions from a datasource</param>
        /// <param name="typeDiscoverer">A <see cref="ITypeDiscoverer"/> for discovering <see cref="IEventSubscriber"/>s in current process</param>
        /// <param name="container">A <see cref="IContainer"/> for creating instances of objects/services</param>
        public EventSubscriptionManager(IEventSubscriptionRepository repository, ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _repository = repository;
            _typeDiscoverer = typeDiscoverer;
            _container = container;

            Initialize();
        }

        void Initialize()
        {
            _allSubscriptions = new List<EventSubscription>();
            _availableSubscriptions = new List<EventSubscription>();
            CollectAvailableSubscribers();
            MergeSubscribersFromRepository();
        }

#pragma warning disable 1591 // Xml Comments

        public IEnumerable<EventSubscription> GetAllSubscriptions()
        {
            return _allSubscriptions;
        }

        public IEnumerable<EventSubscription> GetAvailableSubscriptions()
        {
            var availableSubscriptions = _allSubscriptions.Where(s => _availableSubscriptions.Contains(s));
            return availableSubscriptions;
        }

        public void Process(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
                Process(@event);
            
        }

        public void Process(IEvent @event)
        {
            var eventType = @event.GetType();
            var subscriptionsToProcess = _availableSubscriptions.Where(s => s.EventType.Equals(eventType));
            foreach (var subscriptionToProcess in subscriptionsToProcess)
            {
                if (!subscriptionToProcess.ShouldProcess(@event))
                    continue;

                var subscriberType = subscriptionToProcess.Owner;
                var method = subscriptionToProcess.Method;
                var subscriberInstance = _container.Get(subscriberType);
                method.Invoke(subscriberInstance, new[] { @event });

                subscriptionToProcess.LastEventId = @event.Id;
                _repository.Update(subscriptionToProcess);
            }
        }
#pragma warning restore 1591 // Xml Comments
        void CollectAvailableSubscribers()
        {
            var eventSubscriberTypes = _typeDiscoverer.FindMultiple<IEventSubscriber>();

            foreach (var eventSubscriberType in eventSubscriberTypes)
            {
                var subscribers = (from m in eventSubscriberType.GetMethods()
                                  where m.Name == ProcessMethodInvoker.ProcessMethodName &&
                                        m.GetParameters().Length == 1 &&
                                        typeof(IEvent).IsAssignableFrom(m.GetParameters()[0].ParameterType)
                                  select new EventSubscription
                                  {
                                      Owner = eventSubscriberType,
                                      Method = m,
                                      EventType = m.GetParameters()[0].ParameterType,
                                      EventName = m.GetParameters()[0].ParameterType.Name,
                                      LastEventId = 0
                                  }).ToArray();

                _availableSubscriptions.AddRange(subscribers);
                _allSubscriptions.AddRange(subscribers);
            }
        }

        void MergeSubscribersFromRepository()
        {
            var subscribersFromRepository = _repository.GetAll();
            var subscribersToUpdate = subscribersFromRepository.Where(s => _availableSubscriptions.Where(ss => ss.Equals(s)).Count() == 1);
            foreach (var subscriber in subscribersToUpdate)
            {
                var subscriberToUpdate = _availableSubscriptions.Where(s => s.Equals(subscriber)).Single();
                if( subscriber.LastEventId > subscriberToUpdate.LastEventId )
                    subscriberToUpdate.LastEventId = subscriber.LastEventId;
            }
            var subscribersNotInProcess = subscribersFromRepository.Where(s => !_availableSubscriptions.Contains(s));
            _allSubscriptions.AddRange(subscribersNotInProcess);

            var subscribersNotInRepository = _availableSubscriptions.Where(s => !subscribersFromRepository.Contains(s));
            foreach (var subscriber in subscribersNotInRepository)
                _repository.Add(subscriber);
        }

    }
}
