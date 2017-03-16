/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.MSpec.Events
{
    public class EventSequenceValidation<T> where T : IEvent
    {
        readonly UncommittedEventStream _stream;

        public EventSequenceValidation(UncommittedEventStream stream)
        {
            _stream = stream;
        }

        public EventValueValidation<T> AtSequenceNumber(int sequenceNumber)
        {
            var query = from e in _stream.EventsAndVersion
                        where e.Version.Sequence == sequenceNumber
                        select e;

            var @event = (T)query.FirstOrDefault().Event;
            @event.ShouldNotBeNull();
            @event.ShouldBeOfExactType<T>();
            return new EventValueValidation<T>(@event);
        }

        public EventValueValidation<T> InStream()
        {
            var foundEvent = default(T);
            foreach (var @event in _stream)
            {
                if (@event.GetType().Equals(typeof(T)))
                {
                    foundEvent = (T)@event;
                }
            }
            foundEvent.ShouldNotBeNull();
            return new EventValueValidation<T>(foundEvent);


        }

        public EventValueValidation<T> AtBeginning()
        {
            var @event = (T)_stream.FirstOrDefault();
            @event.ShouldNotBeNull();
            @event.ShouldBeOfExactType<T>();
            return new EventValueValidation<T>(@event);
        }

        public EventValueValidation<T> AtEnd()
        {
            var @event = (T)_stream.LastOrDefault();
            @event.ShouldNotBeNull();
            @event.ShouldBeOfExactType<T>();
            return new EventValueValidation<T>(@event);

        }
    }
}