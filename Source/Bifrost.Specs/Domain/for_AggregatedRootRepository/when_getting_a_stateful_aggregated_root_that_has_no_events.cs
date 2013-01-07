using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Domain.for_AggregatedRootRepository
{
    [Subject(Subjects.getting_aggregated_root)]
    public class when_getting_a_stateful_aggregated_root_that_has_no_events : given.a_repository_for_a_stateful_aggregated_root
    {
        static Guid aggregated_root_id = Guid.NewGuid();
        static Guid event_id = Guid.NewGuid();
        static SimpleStatefulAggregatedRoot stateful_aggregated_root;
        static CommittedEventStream event_stream;
        static EventSourceVersion expected_version;

        Establish context = () =>
                                {
                                    expected_version = new EventSourceVersion(0,0);
                                    event_stream = new CommittedEventStream(aggregated_root_id);
                                    event_store_mock.Setup(e => e.GetForEventSource(Moq.It.IsAny<EventSource>(), Moq.It.IsAny<Guid>())).Returns(
                                        event_stream);
                                };

        Because of = () => stateful_aggregated_root = repository.Get(aggregated_root_id);

        It should_return_an_instance = () => stateful_aggregated_root.ShouldNotBeNull();
        It should_get_events_for_the_aggregated_root = () => event_store_mock.Verify(e => e.GetForEventSource(Moq.It.IsAny<EventSource>(), aggregated_root_id));
        It should_not_re_apply_any_events_for_the_aggregated_root = () => stateful_aggregated_root.ReApplyCalled.ShouldBeFalse();
        It should_be_the_initial_version = () => stateful_aggregated_root.Version.ShouldEqual(expected_version);
        It should_register_the_aggregate_root_for_tracking_within_this_context = () => command_context_mock.Verify(cc => cc.RegisterForTracking(stateful_aggregated_root));
    }
}