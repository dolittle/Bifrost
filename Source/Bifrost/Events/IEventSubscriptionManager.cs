/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a manager for dealing with <see cref="EventSubscription">EventSubscriptions</see>
    /// </summary>
    public interface IEventSubscriptionManager
    {
        /// <summary>
        /// Get all <see cref="EventSubscription">EventSubscriptions</see>
        /// </summary>
        /// <returns>All available <see cref="EventSubscription">EventSubscriptions</see></returns>
        IEnumerable<EventSubscription> GetAllSubscriptions();

        /// <summary>
        /// Get all available <see cref="EventSubscription">EventSubscriptions</see>
        /// </summary>
        /// <returns>All available <see cref="EventSubscription">EventSubscriptions</see></returns>
        IEnumerable<EventSubscription> GetAvailableSubscriptions();

        /// <summary>
        /// Process a set of <see cref="EventWithEnvelope">Events</see> for a specific subscription
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to process events for</param>
        /// <param name="eventsWithEnvelope"><see cref="EventWithEnvelope">Events</see> to process</param>
        void Process(EventSubscription subscription, IEnumerable<EventWithEnvelope> eventsWithEnvelope);

        /// <summary>
        /// Process a single <see cref="EventWithEnvelope"/> for a specific subscription
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to process event for</param>
        /// <param name="eventAndEnvelope"><see cref="EventWithEnvelope"/> to process</param>
        void Process(EventSubscription subscription, EventWithEnvelope eventAndEnvelope);

        /// <summary>
        /// Process a set of <see cref="EventWithEnvelope">Events</see>
        /// </summary>
        /// <param name="eventsWithEnvelope"><see cref="EventWithEnvelope">Events</see> to process</param>
        void Process(IEnumerable<EventWithEnvelope> eventsWithEnvelope);

        /// <summary>
        /// Process a single <see cref="EventWithEnvelope"/>
        /// </summary>
        /// <param name="eventAndEnvelope"><see cref="EventWithEnvelope"/> to process</param>
        void Process(EventWithEnvelope eventAndEnvelope);
    }
}
