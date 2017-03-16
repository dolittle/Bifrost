/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Concepts;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents the identification of an <see cref="IEvent"/>
    /// </summary>
    public class EventSequenceNumber : ConceptAs<long>
    {
        /// <summary>
        /// Represents a null Event - EventId *MUST* start with 1
        /// </summary>
        public static EventSequenceNumber Zero = 0L;

        /// <summary>
        /// Implicitly convert from a <see cref="long"/> to an <see cref="EventSequenceNumber"/>
        /// </summary>
        /// <param name="eventSequenceNumber">Actual sequence number</param>
        public static implicit operator EventSequenceNumber(long eventSequenceNumber)
        {
            return new EventSequenceNumber { Value = eventSequenceNumber };
        }
    }
}
