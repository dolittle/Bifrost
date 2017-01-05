/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a system for working with <see cref="IEventSubscription">Event Subscriptions</see>
    /// </summary>
    public interface IEventSubscriptions
    {
        /// <summary>
        /// Get all subscriptions available
        /// </summary>
        /// <returns>All available <see cref="EventSubscription">EventSubscriptions</see> </returns>
        IEnumerable<EventSubscription> GetAll();

        /// <summary>
        /// Save the state of an event subscription
        /// </summary>
        /// <param name="subscription"><see cref="EventSubscription"/> to save</param>
        void Save(EventSubscription subscription);

        /// <summary>
        /// Reset last event id for all subscriptions
        /// </summary>
        void ResetLastEventForAllSubscriptions();
    }
}
