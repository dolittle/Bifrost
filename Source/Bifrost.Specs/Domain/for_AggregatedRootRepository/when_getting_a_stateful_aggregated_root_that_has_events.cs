using System;
using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Domain.for_AggregatedRootRepository
{
    [Subject(Subjects.getting_aggregated_root)]
    public class when_getting_a_stateful_aggregated_root_that_has_events : given.a_repository_for_a_stateful_aggregated_root
    {
        static Guid aggregated_root_id = Guid.NewGuid();
        static Guid event_id = Guid.NewGuid();
        static SimpleStatefulAggregatedRoot stateful_aggregated_root;
        static CommittedEventStream event_stream;
        static EventSourceVersion expected_version;
        static EventSourceVersion version;

        Establish context = () =>
        {
            var @event = new SimpleEvent(aggregated_root_id, event_id);
            @event.Version = @event.Version.NextCommit().NextSequence();

            expected_version = new EventSourceVersion(2,0);

            event_stream = new CommittedEventStream(aggregated_root_id);
            event_stream.Append(new List<IEvent>(){@event});
            event_store_mock.Setup(e => e.Load(Moq.It.IsAny<Type>(), Moq.It.IsAny<Guid>())).Returns(
                event_stream);
        };

        Because of = () =>
                         {
                             stateful_aggregated_root = repository.Get(aggregated_root_id);
                             version = stateful_aggregated_root.Version;
                         };

        It should_return_an_instance = () => stateful_aggregated_root.ShouldNotBeNull();
        It should_get_events_for_the_aggregated_root = () => event_store_mock.Verify(e => e.Load(typeof(SimpleStatefulAggregatedRoot), aggregated_root_id));
        It should_re_apply_events_for_the_aggregated_root = () => stateful_aggregated_root.ReApplyCalled.ShouldBeTrue();
        It should_be_the_correct_version = () => version.ShouldEqual(expected_version);
        It should_register_the_aggregate_root_for_tracking_within_this_context = () => command_context_mock.Verify(cc => cc.RegisterForTracking(stateful_aggregated_root));
    }
}
