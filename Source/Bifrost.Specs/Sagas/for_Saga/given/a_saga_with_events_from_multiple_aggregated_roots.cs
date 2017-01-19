using System;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga.given
{
    public class a_saga_with_events_from_multiple_aggregated_roots : a_saga
    {
        protected static Guid first_aggregated_root_id = Guid.NewGuid();
        protected static Guid second_aggregated_root_id = Guid.NewGuid();
        protected static SimpleEvent first_event;
        protected static SimpleEvent second_event;

        Establish context = () =>
        {
            var firstEventStream = new UncommittedEventStream(first_aggregated_root_id);
            first_event = new SimpleEvent(first_aggregated_root_id);
            firstEventStream.Append(first_event);

            var secondEventStream = new UncommittedEventStream(second_aggregated_root_id);
            second_event = new SimpleEvent(second_aggregated_root_id);
            secondEventStream.Append(second_event);

            saga.Commit(firstEventStream);
            saga.Commit(secondEventStream);
        };
    }
}