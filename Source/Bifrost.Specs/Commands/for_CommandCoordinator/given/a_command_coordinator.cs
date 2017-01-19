using Bifrost.Commands;
using Bifrost.Security;
using Bifrost.Validation;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandCoordinator.given
{
    public abstract class a_command_coordinator : Globalization.given.a_localizer_mock
    {
        protected static CommandCoordinator coordinator;
        protected static Mock<ICommandHandlerManager> command_handler_manager_mock;
        protected static Mock<ICommandContextManager> command_context_manager_mock;
        protected static Mock<ICommandSecurityManager> command_security_manager_mock;
        protected static Mock<ICommandValidators> command_validators_mock;
        protected static Mock<ICommand> command_mock;
        protected static Mock<ICommandContext> command_context_mock;

        Establish context = () =>
                                {
                                    command_mock = new Mock<ICommand>();
                                    command_handler_manager_mock = new Mock<ICommandHandlerManager>();
                                    command_context_manager_mock = new Mock<ICommandContextManager>();
                                    command_validators_mock = new Mock<ICommandValidators>();

                                    command_context_mock = new Mock<ICommandContext>();
                                    command_context_manager_mock.Setup(c => c.EstablishForCommand(Moq.It.IsAny<ICommand>())).
                                        Returns(command_context_mock.Object);
                                    command_security_manager_mock = new Mock<ICommandSecurityManager>();
                                    command_security_manager_mock.Setup(
                                        s => s.Authorize(Moq.It.IsAny<ICommand>()))
                                                                 .Returns(new AuthorizationResult());

                                    coordinator = new CommandCoordinator(
                                        command_handler_manager_mock.Object,
                                        command_context_manager_mock.Object,
                                        command_security_manager_mock.Object,
                                        command_validators_mock.Object,
                                        localizer_mock.Object
                                        );
                                };
    }
}
