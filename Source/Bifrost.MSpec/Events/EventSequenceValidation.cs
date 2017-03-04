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
        readonly EventStream _stream;

        public EventSequenceValidation(EventStream stream)
        {
            _stream = stream;
        }

        public EventValueValidation<T> AtSequenceNumber(int sequenceNumber)
        {
            var query = from e in _stream
                        where e.Envelope.Version.Sequence == sequenceNumber
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
                    foundEvent = (T)@event.Event;
                }
            }
            foundEvent.ShouldNotBeNull();
            return new EventValueValidation<T>(foundEvent);


        }

        public EventValueValidation<T> AtBeginning()
        {
            var @event = (T)_stream.FirstOrDefault().Event;
            @event.ShouldNotBeNull();
            @event.ShouldBeOfExactType<T>();
            return new EventValueValidation<T>(@event);
        }

        public EventValueValidation<T> AtEnd()
        {
            var @event = (T)_stream.LastOrDefault().Event;
            @event.ShouldNotBeNull();
            @event.ShouldBeOfExactType<T>();
            return new EventValueValidation<T>(@event);

        }
    }
}