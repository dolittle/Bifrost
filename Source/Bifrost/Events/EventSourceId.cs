/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Concepts;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the identification of an <see cref="IEventSource"/>
    /// </summary>
    public class EventSourceId : ConceptAs<Guid>
    {
        /// <summary>
        /// Creates a new instance of <see cref="EventSourceId"/> with a unique id
        /// </summary>
        /// <returns>A new <see cref="EventSourceId"/></returns>
        public static EventSourceId New()
        {
            return new EventSourceId { Value = Guid.NewGuid() };
        }

        /// <summary>
        /// Implicitly convert from a <see cref="Guid"/> to an <see cref="EventSourceId"/>
        /// </summary>
        /// <param name="eventId"></param>
        public static implicit operator EventSourceId(Guid eventId)
        {
            return new EventSourceId { Value = eventId };
        }
    }
}
