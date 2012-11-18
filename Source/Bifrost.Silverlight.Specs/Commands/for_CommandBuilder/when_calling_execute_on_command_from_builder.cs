using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_calling_execute_on_command_from_builder : given.an_empty_command_builder
    {
        static ICommand command;

        Establish context = () =>
        {
            builder.Name = "Test";
            command = builder.GetInstance();
        };

        Because of = () => command.Execute(null);

        It should_forward_itself_to_the_command_coordinators_handle = () => command_coordinator_mock.Verify(c => c.Handle(command), Times.Once());
    }
}
