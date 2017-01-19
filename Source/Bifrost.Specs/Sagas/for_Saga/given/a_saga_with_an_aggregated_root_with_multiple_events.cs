using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga.given
{
    public class a_saga_with_an_aggregated_root_with_multiple_events : a_saga
    {
        protected static Guid aggregated_root_id = Guid.NewGuid();
        protected static SimpleEvent first_event;
        protected static SimpleEvent second_event;

        Establish context = () =>
        {
            var eventStream = new UncommittedEventStream(aggregated_root_id);
            first_event = new SimpleEvent(aggregated_root_id) { Version = new EventSourceVersion(1, 0) };
            eventStream.Append(first_event);
            second_event = new SimpleEvent(aggregated_root_id) { Version = new EventSourceVersion(1, 1) };
            eventStream.Append(second_event);

            saga.Commit(eventStream);
        };
    }
}