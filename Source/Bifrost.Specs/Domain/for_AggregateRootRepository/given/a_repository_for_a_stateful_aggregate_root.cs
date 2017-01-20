using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Events;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository.given
{
    public class a_repository_for_a_stateful_aggregate_root
    {
        protected static AggregateRootRepository<SimpleStatefulAggregateRoot> repository;
        protected static Mock<ICommandContext> command_context_mock;
        protected static Mock<ICommandContextManager> command_context_manager_mock;

        Establish context = () =>
                            {
                                command_context_mock = new Mock<ICommandContext>();
                                command_context_manager_mock = new Mock<ICommandContextManager>();
                                repository = new AggregateRootRepository<SimpleStatefulAggregateRoot>(command_context_manager_mock.Object);
                                command_context_manager_mock.Setup(ccm => ccm.GetCurrent()).Returns(command_context_mock.Object);
                            };
    }
}
