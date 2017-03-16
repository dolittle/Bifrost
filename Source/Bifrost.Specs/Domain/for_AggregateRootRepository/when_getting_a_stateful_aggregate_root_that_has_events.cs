using System;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    [Subject(typeof(AggregateRootRepository<>))]
    public class when_getting_a_stateful_aggregate_root_that_has_events : given.a_repository_for_a_stateful_aggregate_root
    {
        static EventSourceId event_source_id = Guid.NewGuid();
        static long event_id = 42;
        static SimpleStatefulAggregateRoot stateful_aggregated_root;
        static CommittedEventStream event_stream;
        static EventSourceVersion expected_version;

        Establish context = () =>
        {
            var @event = new SimpleEvent(event_source_id);
            var version = new EventSourceVersion(1, 1);
            var event_envelope = new Mock<IEventEnvelope>();
            event_envelope.SetupGet(e => e.Version).Returns(version);

            expected_version = new EventSourceVersion(2,0);

            event_stream = new CommittedEventStream(event_source_id, new[] {
                new EventAndEnvelope(event_envelope.Object, @event)
            });
            command_context_mock.Setup(e => e.GetCommittedEventsFor(Moq.It.IsAny<EventSource>()))
                .Returns(event_stream);
        };

        Because of = () => stateful_aggregated_root = repository.Get(event_source_id);

        It should_return_an_instance = () => stateful_aggregated_root.ShouldNotBeNull();
        It should_get_events_for_the_aggregated_root = () => command_context_mock.Verify(e => e.GetCommittedEventsFor(Moq.It.IsAny<EventSource>()));
        It should_re_apply_events_for_the_aggregated_root = () => stateful_aggregated_root.ReApplyCalled.ShouldBeTrue();
        It should_be_the_correct_version = () => stateful_aggregated_root.Version.ShouldEqual(expected_version);
        It should_register_the_aggregate_root_for_tracking_within_this_context = () => command_context_mock.Verify(cc => cc.RegisterForTracking(stateful_aggregated_root));
    }
}
