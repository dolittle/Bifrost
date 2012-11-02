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
using Bifrost.Entities;
using Bifrost.Serialization;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSubscriptionRepository"/>
    /// </summary>
    public class EventSubscriptionRepository : IEventSubscriptionRepository
    {
        readonly IEntityContext<EventSubscriptionHolder> _entityContext;
        readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="EventSubscriptionRepository"/>
        /// </summary>
        /// <param name="entityContext">An <see cref="IEntityContext{EventSubscriptionHolder}"/> for working with persisting of <see cref="EventSubscriptionHolder">EventSubscriptionHolders</see></param>
        /// <param name="serializer">A <see cref="ISerializer"/> to use for serialization</param>
        public EventSubscriptionRepository(IEntityContext<EventSubscriptionHolder> entityContext, ISerializer serializer)
        {
            _entityContext = entityContext;
            _serializer = serializer;
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<EventSubscription> GetForEvent(Type eventType)
        {
            var all = _entityContext.Entities.ToArray();
            var filtered = all.Where(e=>
                e.EventType == eventType.AssemblyQualifiedName &&
                EventTypeExists(e) &&
                EventSubscriberExists(e)).ToArray();
            
            return filtered.Select(ConvertToEventSubscription).ToArray();
        }

        public IEnumerable<EventSubscription> GetAll()
        {
            var all = _entityContext.Entities.ToArray();
            var filtered = all.Where(e =>
                EventTypeExists(e) &&
                EventSubscriberExists(e)).ToArray();

            return filtered.Select(ConvertToEventSubscription).ToArray();
        }

        public EventSubscription Get(Guid id)
        {
            var subscription = _entityContext.Entities.Where(e => e.Id == id).Select(ConvertToEventSubscription).Single();
            return subscription;
        }

        public void ResetLastEventId(Guid id)
        {
            var subscription = _entityContext.Entities.Where(e => e.Id == id).Single();
            subscription.LastEventId = 0;
            _entityContext.Update(subscription);
            _entityContext.Commit();
        }



        public void Add(EventSubscription subscription)
        {
            var holder = ConvertToEventSubscriptionHolder(subscription);
            _entityContext.Insert(holder);
            _entityContext.Commit();
        }

        public void Update(EventSubscription subscription)
        {
            var holder = GetHolderFromSubscription(subscription);
            if (holder == null)
                Add(subscription);
            else
            {
                CopyToEventSubscriptionHolder(subscription, holder);
                _entityContext.Update(holder);
                _entityContext.Commit();
            }
        }
#pragma warning restore 1591 // Xml Comments


        bool EventTypeExists(EventSubscriptionHolder holder)
        {
            var eventType = Type.GetType(holder.EventType);
            return eventType != null;
        }

        bool EventSubscriberExists(EventSubscriptionHolder holder)
        {
            var ownerType = Type.GetType(holder.Owner);
            return ownerType != null;
        }

        EventSubscriptionHolder GetHolderFromSubscription(EventSubscription subscription)
        {
            var specificHolder = ConvertToEventSubscriptionHolder(subscription);
            var holder = _entityContext.Entities.Where(e =>
                e.Owner == specificHolder.Owner &&
                e.Method == specificHolder.Method &&
                e.EventType == specificHolder.EventType).SingleOrDefault();
            return holder;
        }


        EventSubscriptionHolder ConvertToEventSubscriptionHolder(EventSubscription subscription)
        {
            var holder = new EventSubscriptionHolder();
            CopyToEventSubscriptionHolder(subscription, holder);
            return holder;
        }

        void CopyToEventSubscriptionHolder(EventSubscription subscription, EventSubscriptionHolder holder)
        {
            holder.Id = subscription.Id;
            holder.LastEventId = subscription.LastEventId;
            holder.Owner = subscription.Owner.AssemblyQualifiedName;
            holder.Method = subscription.Method.Name;
            holder.EventType = subscription.EventType.AssemblyQualifiedName;
            holder.EventName = subscription.EventName;
            holder.LastEventId = subscription.LastEventId;
        }


        EventSubscription ConvertToEventSubscription(EventSubscriptionHolder holder)
        {
            var eventType = Type.GetType(holder.EventType);
            var ownerType = Type.GetType(holder.Owner);
            return new EventSubscription
            {
                Id = holder.Id,
                EventName = holder.EventName,
                EventType = eventType,
                Owner = ownerType,
                Method = ownerType.GetMethod(ProcessMethodInvoker.ProcessMethodName, new[] { eventType }),
                LastEventId = holder.LastEventId
            };
        }
    }
}
