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
using Bifrost.Entities;
using Bifrost.Serialization;
#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSubscriptionRepository"/>
    /// </summary>
    public class EventSubscriptionRepository : IEventSubscriptionRepository
    {
        readonly IEntityContext<EventSubscription> _entityContext;

        /// <summary>
        /// Initializes a new instance of <see cref="EventSubscriptionRepository"/>
        /// </summary>
        /// <param name="entityContext">An <see cref="IEntityContext{EventSubscriptionHolder}"/> for working with persisting of <see cref="EventSubscriptionHolder">EventSubscriptionHolders</see></param>
        public EventSubscriptionRepository(IEntityContext<EventSubscription> entityContext)
        {
            _entityContext = entityContext;
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<EventSubscription> GetForEvent(Type eventType)
        {
            var all = _entityContext.Entities.ToArray();
            var filtered = all.Where(e=>
                e.EventType == eventType &&
                EventTypeExists(e) &&
                EventSubscriberExists(e)).ToArray();
            
            return filtered.ToArray();
        }

        public IEnumerable<EventSubscription> GetAll()
        {
            var all = _entityContext.Entities.ToArray();
            var filtered = all.Where(e =>
                EventTypeExists(e) &&
                EventSubscriberExists(e)).ToArray();

            return filtered.ToArray();
        }

        public EventSubscription Get(Guid id)
        {
            var subscription = _entityContext.Entities.Where(e => e.Id == id).Single();
            return subscription;
        }

        public void ResetLastEventId(Guid id)
        {
            var subscription = _entityContext.Entities.Where(e => e.Id == id).Single();
            subscription.LastEventId = 0;
            _entityContext.Update(subscription);
            _entityContext.Commit();
        }

        public void ResetLastEventForAllSubscriptions()
        {
            foreach (var subscription in _entityContext.Entities)
            {
                subscription.LastEventId = 0;
                _entityContext.Update(subscription);
            }
            _entityContext.Commit();
        }


        public void Add(EventSubscription subscription)
        {
            _entityContext.Insert(subscription);
            _entityContext.Commit();
        }

        public void Update(EventSubscription subscription)
        {
            _entityContext.Update(subscription);
            _entityContext.Commit();
        }
#pragma warning restore 1591 // Xml Comments


        bool EventTypeExists(EventSubscription subscription)
        {
            return subscription.EventType != null;
        }

        bool EventSubscriberExists(EventSubscription subscription)
        {
            return subscription.Owner != null;
        }



    }
}
