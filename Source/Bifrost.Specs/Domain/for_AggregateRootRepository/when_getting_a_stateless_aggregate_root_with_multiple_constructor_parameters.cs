using System;
using Bifrost.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    public class when_getting_a_stateless_aggregate_root_with_multiple_constructor_parameters : given.a_command_context
    {
        protected static AggregateRootRepository<AggregateRootWithMultipleConstructorParameters> repository;
        protected static Exception result;

        Establish context = () => repository = new AggregateRootRepository<AggregateRootWithMultipleConstructorParameters>(command_context_manager.Object);

        Because of = () => result = Catch.Exception(() => repository.Get(Guid.NewGuid()));

        It should_throw_invalid_aggregate_root_constructor_signature = () => result.ShouldBeOfExactType<InvalidAggregateRootConstructorSignature>();
    }
}
