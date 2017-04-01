using System;
using Bifrost.Domain;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    [Subject(typeof(AggregateRootRepository<>))]
    public class when_getting_a_stateless_aggregate_root_that_has_events : given.a_repository_for_a_stateless_aggregate_root
    {
        static EventSourceId event_source_id = Guid.NewGuid();
        static EventId event_id = Guid.NewGuid();
        static SimpleStatelessAggregateRoot stateless_aggregated_root;
        static EventSourceVersion expected_version;
        static EventSourceVersion version_of_last_event;

        Establish context = () =>
                                {
                                    version_of_last_event = new EventSourceVersion(1,1);
                                    expected_version = new EventSourceVersion(2, 0);
                                    event_source_versions.Setup(e => e.GetFor(application_resource_identifier.Object, event_source_id)).Returns(version_of_last_event);
                                };

        Because of = () => stateless_aggregated_root = repository.Get(event_source_id);

        It should_return_an_instance = () => stateless_aggregated_root.ShouldNotBeNull();
        It should_not_get_events_for_the_aggregated_root = () => event_store.Verify(e => e.GetFor(application_resource_identifier.Object, event_source_id), Moq.Times.Never());
        It should_not_re_apply_events_for_the_aggregated_root = () => stateless_aggregated_root .ReApplyCalled.ShouldBeFalse();
        It should_ensure_the_event_source_has_the_correct_version = () => stateless_aggregated_root.Version.ShouldEqual(expected_version);
        It should_register_the_aggregate_root_for_tracking_within_this_context = () => command_context_mock.Verify(cc => cc.RegisterForTracking(stateless_aggregated_root));
    }
}