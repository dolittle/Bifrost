/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Events;

namespace Bifrost.MSpec.Events
{
    public static class EventStreamValidation
    {
        public static EventSequenceValidation<T> ShouldHaveEvent<T>(this EventSource eventSource) where T : IEvent
        {
            var sequenceValidation = new EventSequenceValidation<T>(eventSource.UncommittedEvents);
            return sequenceValidation;
        }
    }
}
