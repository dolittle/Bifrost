using System;
using Bifrost.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    public class when_getting_a_stateless_aggregate_root_with_event_source_id_in_constructor : given.a_command_context
    {
        protected static AggregateRootRepository<AggregateRootWithEventSourceIdConstructor> repository;
        protected static AggregateRootWithEventSourceIdConstructor result;

        Establish context = () => repository = new AggregateRootRepository<AggregateRootWithEventSourceIdConstructor>(command_context_manager.Object, event_envelopes.Object);

        Because of = () => result = repository.Get(Guid.NewGuid());

        It should_return_an_instance = () => result.ShouldNotBeNull();
    }
}
