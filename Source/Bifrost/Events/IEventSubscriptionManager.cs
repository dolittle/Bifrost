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
        /// Process a set of <see cref="EventEnvelopeAndEvent">Events</see> for a specific subscription
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to process events for</param>
        /// <param name="eventsWithEnvelope"><see cref="EventEnvelopeAndEvent">Events</see> to process</param>
        void Process(EventSubscription subscription, IEnumerable<EventEnvelopeAndEvent> eventsWithEnvelope);

        /// <summary>
        /// Process a single <see cref="EventEnvelopeAndEvent"/> for a specific subscription
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to process event for</param>
        /// <param name="eventAndEnvelope"><see cref="EventEnvelopeAndEvent"/> to process</param>
        void Process(EventSubscription subscription, EventEnvelopeAndEvent eventAndEnvelope);

        /// <summary>
        /// Process a set of <see cref="EventEnvelopeAndEvent">Events</see>
        /// </summary>
        /// <param name="eventsWithEnvelope"><see cref="EventEnvelopeAndEvent">Events</see> to process</param>
        void Process(IEnumerable<EventEnvelopeAndEvent> eventsWithEnvelope);

        /// <summary>
        /// Process a single <see cref="EventEnvelopeAndEvent"/>
        /// </summary>
        /// <param name="eventAndEnvelope"><see cref="EventEnvelopeAndEvent"/> to process</param>
        void Process(EventEnvelopeAndEvent eventAndEnvelope);
    }
}
