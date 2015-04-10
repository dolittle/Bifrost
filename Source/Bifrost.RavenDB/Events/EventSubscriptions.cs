#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Transactions;
using Bifrost.Events;
using Raven.Abstractions.Exceptions;
using Raven.Client.Document;

namespace Bifrost.RavenDB.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventSubscriptions"/>
    /// </summary>
    public class EventSubscriptions : IEventSubscriptions
    {
        DocumentStore _documentStore;

        public EventSubscriptions(IEventSubscriptionsConfiguration configuration)
        {
            _documentStore = configuration.CreateDocumentStore();
        }

        public IEnumerable<EventSubscription> GetAll()
        {
            using (var session = _documentStore.OpenSession())
                return session.Query<EventSubscription>();
        }

        public void Save(EventSubscription subscription)
        {
            using (var session = _documentStore.OpenSession())
            {
                using (new TransactionScope(TransactionScopeOption.Suppress))
                {
                    var saving = true;

                    while (saving)
                    {
                        try
                        {
                            session.Store(subscription);
                            session.SaveChanges();
                            saving = false;
                        }
                        catch (ConcurrencyException)
                        {
                            var existing = session.Load<EventSubscription>(subscription.Id);
                            if (existing.LastEventId > subscription.LastEventId)
                                saving = false;
                        }
                    }
                }
            }
        }

        public void ResetLastEventForAllSubscriptions()
        {
            using (var session = _documentStore.OpenSession())
            {
                foreach (var subscription in session.Query<EventSubscription>())
                    subscription.LastEventId = 0;

                session.SaveChanges();
            }
        }
    }
}
