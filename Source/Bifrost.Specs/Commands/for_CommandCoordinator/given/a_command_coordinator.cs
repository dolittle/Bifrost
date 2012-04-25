﻿using Bifrost.Commands;
using Bifrost.Sagas;
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
        protected static Mock<ICommandValidationService> command_validation_service_mock;
        protected static Mock<IDynamicCommandFactory> dynamic_command_factory_mock; 
        protected static Mock<ICommand> command_mock;

        Establish context = () =>
                                {
                                    command_mock = new Mock<ICommand>();
                                    command_handler_manager_mock = new Mock<ICommandHandlerManager>();
                                    command_context_manager_mock = new Mock<ICommandContextManager>();
                                    command_validation_service_mock = new Mock<ICommandValidationService>();
                                    dynamic_command_factory_mock = new Mock<IDynamicCommandFactory>();
                                    var commandContextMock = new Mock<ICommandContext>();
                                    command_context_manager_mock.Setup(c => c.EstablishForCommand(Moq.It.IsAny<ICommand>())).
                                        Returns(commandContextMock.Object);
                                    command_context_manager_mock.Setup(c => c.EstablishForSaga(Moq.It.IsAny<ISaga>(),Moq.It.IsAny<ICommand>()))
                                        .Returns(commandContextMock.Object);

                                    coordinator = new CommandCoordinator(
                                        command_handler_manager_mock.Object,
                                        command_context_manager_mock.Object,
                                        command_validation_service_mock.Object,
                                        dynamic_command_factory_mock.Object,
										localizer_mock.Object
                                        );
                                };
    }
}
