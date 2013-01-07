using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Fakes.Domain;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStore.given
{
    public class a_persisted_stream_of_20_events_belonging_to_2_different_aggregate_roots : Globalization.given.a_localizer_mock
    {
		protected static IQueryable<IEvent> persisted_events;
        protected static Guid event_source_id;
        protected static Guid other_event_source_id;
        protected static IEvent last_event_for_event_source;
        protected static IEvent last_event_for_other_event_source;

        Establish context =
            () =>
            {
                event_source_id = Guid.NewGuid();
                other_event_source_id = Guid.NewGuid();
				var eventEntities = new List<IEvent>();
                for(var i = 0; i < 20; i++)
                {
                    var aggregateId = i%2 == 0 ? event_source_id : other_event_source_id;
                    var @event = new SimpleEvent(aggregateId)
                                     {
                                         EventSource = typeof(StatefulAggregatedRoot).AssemblyQualifiedName,
                                         Version = new EventSourceVersion(1,i)
                                     };
					var eventEntity = new SimpleEvent(@event.EventSourceId)
                                     {
										 Id = @event.Id,
                                         EventSource = @event.EventSource,
                                         CausedBy = "Some Command",
                                         Name = @event.Name,
                                         Origin = "Somewhere",
                                     };
                    eventEntities.Add(eventEntity);
                }

                persisted_events = eventEntities.AsQueryable();
                last_event_for_event_source = eventEntities.Where(e => e.EventSourceId == event_source_id).OrderByDescending(e => e.Version).First();
                last_event_for_other_event_source = eventEntities.Where(e => e.EventSourceId == other_event_source_id).OrderByDescending(e => e.Version).First();
            };
    }
}