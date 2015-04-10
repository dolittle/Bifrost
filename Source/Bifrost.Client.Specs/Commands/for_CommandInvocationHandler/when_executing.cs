using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Commands.for_CommandInvocationHandler
{
    public class when_executing
    {
        static Mock<ICommandCoordinator> command_coordinator_mock;
        static Mock<ICanProcessCommandProcess> command_process_processor_mock;
        static Mock<ICommand> command_mock;
        static CommandInvocationHandler handler;
        static Command command;
        static CommandResult result;

        Establish context = () => 
        {
            command_mock = new Mock<ICommand>();

            command_coordinator_mock = new Mock<ICommandCoordinator>();
            command_process_processor_mock = new Mock<ICanProcessCommandProcess>();

            result = new CommandResult();
            command_coordinator_mock.Setup(c => c.Handle(Moq.It.IsAny<ICommand>())).Returns(result);

            handler = new CommandInvocationHandler(command_coordinator_mock.Object);
            command = new Command { Instance = command_mock.Object };
            handler.TargetInstance = command;
            handler.Proxy = command_process_processor_mock.Object;
        };

        Because of = () => handler.Execute(null);

        It should_forward_to_the_command_coordinator = () => command_coordinator_mock.Verify(c => c.Handle(command_mock.Object), Times.Once());
        It should_call_the_processor_with_the_command_and_result = () => command_process_processor_mock.Verify(c => c.Process(command_mock.Object, result), Times.Once());
    }
}
