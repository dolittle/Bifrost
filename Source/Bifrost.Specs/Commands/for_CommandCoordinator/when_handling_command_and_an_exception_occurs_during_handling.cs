using System;
using System.ComponentModel.DataAnnotations;
using Bifrost.Commands;
using Machine.Specifications;
using It = Machine.Specifications.It;
using Bifrost.Validation;

namespace Bifrost.Specs.Commands.for_CommandCoordinator
{
    [Subject(typeof(CommandCoordinator))]
    public class when_handling_command_and_an_exception_occurs_during_handling : given.a_command_coordinator
    {
        static CommandResult result;
        static Exception exception;

        Establish context = () =>
                                {
                                    exception = new Exception();
                                    var validation_results = new CommandValidationResult { ValidationResults = new ValidationResult[0] };
                                    command_validation_service_mock.Setup(cvs => cvs.Validate(command_mock.Object)).Returns(validation_results);
                                    command_handler_manager_mock.Setup(c => c.Handle(Moq.It.IsAny<ICommand>())).Throws(exception);
                                };

        Because of = () => result = coordinator.Handle(command_mock.Object);

        It should_have_validated_the_command = () => command_validation_service_mock.VerifyAll();
        It should_have_authenticated_the_command = () => command_security_manager_mock.VerifyAll();
        It should_have_exception_in_result = () => result.Exception.ShouldEqual(exception);
        It should_have_success_set_to_false = () => result.Success.ShouldBeFalse();
    }
}
