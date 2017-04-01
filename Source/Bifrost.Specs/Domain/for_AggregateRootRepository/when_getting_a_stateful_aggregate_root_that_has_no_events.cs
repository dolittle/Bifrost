using System;
using Bifrost.Domain;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    [Subject(typeof(AggregateRootRepository<>))]
    public class when_getting_a_stateful_aggregate_root_that_has_no_events : given.a_repository_for_a_stateful_aggregate_root
    {
        static EventSourceId event_source_id = Guid.NewGuid();
        static SimpleStatefulAggregateRoot stateful_aggregated_root;
        static EventSourceVersion expected_version;

        Establish context = () =>
                                {
                                    expected_version = EventSourceVersion.Zero;
                                };

        Because of = () => stateful_aggregated_root = repository.Get(event_source_id);

        It should_return_an_instance = () => stateful_aggregated_root.ShouldNotBeNull();
        It should_not_re_apply_any_events_for_the_aggregated_root = () => stateful_aggregated_root.ReApplyCalled.ShouldBeFalse();
        It should_be_the_initial_version = () => stateful_aggregated_root.Version.ShouldEqual(expected_version);
        It should_register_the_aggregate_root_for_tracking_within_this_context = () => command_context_mock.Verify(cc => cc.RegisterForTracking(stateful_aggregated_root));
    }
}