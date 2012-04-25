using Bifrost.Commands;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandCoordinator
{
    [Subject(Subjects.handling_command_for_saga)]
    public class when_handling_a_command_with_success_in_a_saga : given.a_command_coordinator
    {
        Establish a_saga = () =>
        {
            saga_mock = new Mock<ISaga>();
        };

        Because a_command_is_handled_in_context_of_a_saga = () => result = coordinator.Handle(saga_mock.Object, command_mock.Object);

        It should_have_run_the_sage_pre_population = () => saga_mock.Verify(s => s.PrePopulate(command_mock.Object));

        static CommandResult result;
        static Mock<ISaga> saga_mock;
    }
}