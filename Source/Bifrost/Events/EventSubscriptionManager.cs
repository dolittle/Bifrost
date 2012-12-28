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
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;

#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSubscriptionManager"/>
    /// </summary>
    [Singleton]
    public class EventSubscriptionManager : IEventSubscriptionManager
    {
        IEventSubscriptionRepository _repository;
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        IEnumerable<EventSubscription> _subscriptionsFromRepository;
        IEnumerable<EventSubscription> _subscriptionsInProcess;
        List<EventSubscription> _allSubscriptions;

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
            var subscriberInstance = _container.Get(subscription.Owner);
            subscription.Method.Invoke(subscriberInstance, new[] { @event });
            UpdateExistingSubscriptionFrom(subscription, @event.Id);
        }


        public void Process(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
                Process(@event);
        }

        public void Process(IEvent @event)
        {
            MergeSubscribersFromRepository();

            var eventType = @event.GetType();
            var subscriptionsToProcess = _allSubscriptions.Where(s => s.EventType.Equals(eventType));
            foreach (var subscriptionToProcess in subscriptionsToProcess)
            {
                if (!subscriptionToProcess.ShouldProcess(@event))
                    continue;

                Process(subscriptionToProcess, @event);
            }
        }
#pragma warning restore 1591 // Xml Comments

        void UpdateExistingSubscriptionFrom(EventSubscription subscription, long eventId)
        {
            var subscriptionToUpdate = _allSubscriptions.Where(e=>e.Equals(subscription)).Single();
            subscriptionToUpdate.LastEventId = eventId;
            _repository.Update(subscriptionToUpdate);
        }


        void Initialize()
        {
            _allSubscriptions = new List<EventSubscription>();
            CollectInProcessSubscribers();
            MergeSubscribersFromRepository();
        }


        void CollectInProcessSubscribers()
        {
            var subscriptionsInProcess = new List<EventSubscription>();
            var eventSubscriberTypes = _typeDiscoverer.FindMultiple<IEventSubscriber>();

            foreach (var eventSubscriberType in eventSubscriberTypes)
            {
                var subscriptions = (from m in 
#if(NETFX_CORE)
                                       eventSubscriberType.GetTypeInfo().DeclaredMethods
#else
                                       eventSubscriberType.GetMethods()
#endif
                                  where m.Name == ProcessMethodInvoker.ProcessMethodName &&
                                        m.GetParameters().Length == 1 &&
                                        typeof(IEvent)
#if(NETFX_CORE)
                                            .GetTypeInfo().IsAssignableFrom(m.GetParameters()[0].ParameterType.GetTypeInfo())
#else
                                            .IsAssignableFrom(m.GetParameters()[0].ParameterType)
#endif
                                  select new EventSubscription
                                  {
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


        void MergeSubscribersFromRepository()
        {
            _allSubscriptions.Clear();
            _subscriptionsFromRepository = _repository.GetAll();
            AddSubscriptionsFromRepository();
            AddInMemoryOrUseRepositorySubscriptions();
            UpdateInMemorySubscriptions();
            RegisterSubscriptionsThatIsOnlyInMemory();
        }

        void RegisterSubscriptionsThatIsOnlyInMemory()
        {
            var subscribersNotInRepository = _subscriptionsInProcess.Where(s => !_subscriptionsFromRepository.Contains(s));
            foreach (var subscriber in subscribersNotInRepository)
                _repository.Add(subscriber);
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
                subscriberToUpdate.Id = subscriber.Id;
                if (subscriber.LastEventId > subscriberToUpdate.LastEventId)
                    subscriberToUpdate.LastEventId = subscriber.LastEventId;
            }
        }
    }
}
