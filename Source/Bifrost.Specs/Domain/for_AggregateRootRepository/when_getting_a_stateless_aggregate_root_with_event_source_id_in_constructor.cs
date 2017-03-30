using System;
using Bifrost.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    public class when_getting_a_stateless_aggregate_root_with_event_source_id_in_constructor : given.a_command_context
    {
        protected static AggregateRootRepository<AggregateRootWithEventSourceIdConstructor> aggregate_root_repository;
        protected static AggregateRootWithEventSourceIdConstructor result;

        Establish context = () => aggregate_root_repository = new AggregateRootRepository<AggregateRootWithEventSourceIdConstructor>(command_context_manager.Object, event_store.Object, event_source_versions.Object, application_resources.Object);

        Because of = () => result = aggregate_root_repository.Get(Guid.NewGuid());

        It should_return_an_instance = () => result.ShouldNotBeNull();
    }
}
