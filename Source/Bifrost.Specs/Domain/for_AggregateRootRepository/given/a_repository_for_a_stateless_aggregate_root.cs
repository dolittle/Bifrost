using Bifrost.Commands;
using Bifrost.Domain;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository.given
{
    public class a_repository_for_a_stateless_aggregate_root : all_dependencies
    {
        protected static AggregateRootRepository<SimpleStatelessAggregateRoot> repository;
        protected static Mock<ICommandContext> command_context_mock;

        Establish context = () =>
                                {
                                    command_context_mock = new Mock<ICommandContext>();
                                    repository = new AggregateRootRepository<SimpleStatelessAggregateRoot>(command_context_manager.Object, event_envelopes.Object);
                                    command_context_manager.Setup(ccm => ccm.GetCurrent()).Returns(command_context_mock.Object);
                                };
    }
}