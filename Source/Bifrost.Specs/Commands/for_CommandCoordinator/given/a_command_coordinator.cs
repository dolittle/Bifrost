using Bifrost.Commands;
using Bifrost.Diagnostics;
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
        protected static Mock<ICommandValidationService> command_validation_service_mock;
        protected static Mock<ICommand> command_mock;
        protected static Mock<IExceptionPublisher> exception_publisher_mock;

        Establish context = () =>
                                {
                                    command_mock = new Mock<ICommand>();
                                    command_handler_manager_mock = new Mock<ICommandHandlerManager>();
                                    command_context_manager_mock = new Mock<ICommandContextManager>();
                                    command_validation_service_mock = new Mock<ICommandValidationService>();
                                    var commandContextMock = new Mock<ICommandContext>();
                                    command_context_manager_mock.Setup(c => c.EstablishForCommand(Moq.It.IsAny<ICommand>())).
                                        Returns(commandContextMock.Object);
                                    command_security_manager_mock = new Mock<ICommandSecurityManager>();
                                    command_security_manager_mock.Setup(
                                        s => s.Authorize(Moq.It.IsAny<ICommand>()))
                                                                 .Returns(new AuthorizationResult());

                                    exception_publisher_mock = new Mock<IExceptionPublisher>();

                                    coordinator = new CommandCoordinator(
                                        command_handler_manager_mock.Object,
                                        command_context_manager_mock.Object,
                                        command_security_manager_mock.Object,
                                        command_validation_service_mock.Object,
										localizer_mock.Object,
                                        exception_publisher_mock.Object
                                        );
                                };
    }
}
