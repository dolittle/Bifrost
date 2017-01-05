/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
