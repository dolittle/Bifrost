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
        /// Implicitly convert from a <see cref="long"/> to an <see cref="EventId"/>
        /// </summary>
        /// <param name="eventId"></param>
        public static implicit operator EventId(Guid eventId)
        {
            return new EventId { Value = eventId };
        }
    }
}
