using Bifrost.Domain;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_SagaCommandContext.given
{
    public class a_saga_command_context_with_one_object_being_tracked : a_saga_command_context
    {
        protected static Mock<IAggregateRoot> aggregated_root_mock;

        Establish context = () =>
                                {
                                    aggregated_root_mock = new Mock<IAggregateRoot>();
                                    command_context.RegisterForTracking(aggregated_root_mock.Object);
                                };
    }
}