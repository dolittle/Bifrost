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
        /// Process a set of <see cref="IEvent">Events</see> for a specific subscription
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to process events for</param>
        /// <param name="events"><see cref="IEvent">Events</see> to process</param>
        void Process(EventSubscription subscription, IEnumerable<IEvent> events);

        /// <summary>
        /// Process a single <see cref="IEvent"/> for a specific subscription
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to process event for</param>
        /// <param name="event"><see cref="IEvent"/> to process</param>
        void Process(EventSubscription subscription, IEvent @event);

        /// <summary>
        /// Process a set of <see cref="IEvent">Events</see>
        /// </summary>
        /// <param name="events"><see cref="IEvent">Events</see> to process</param>
        void Process(IEnumerable<IEvent> events);

        /// <summary>
        /// Process a single <see cref="IEvent"/>
        /// </summary>
        /// <param name="event"><see cref="IEvent"/> to process</param>
        void Process(IEvent @event);
    }
}
