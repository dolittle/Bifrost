/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Concepts;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the identification of an <see cref="IEvent"/>
    /// </summary>
    public class EventId : ConceptAs<Guid>
    {
        /// <summary>
        /// Represents a not set <see cref="EventId"/>
        /// </summary>
        public static EventId NotSet = Guid.Empty;

        /// <summary>
        /// Creates a new instance of <see cref="EventId"/> with a unique id
        /// </summary>
        /// <returns>A new <see cref="EventId"/></returns>
        public static EventId New()
        {
            return new EventId { Value = Guid.NewGuid() };
        }

        /// <summary>
        /// Implicitly convert from a <see cref="long"/> to an <see cref="EventId"/>
        /// </summary>
        /// <param name="eventId"></param>
        public static implicit operator EventId(Guid eventId)
        {
            return new EventId { Value = eventId };
        }
    }
}
