using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandCoordinator
{
    [Subject(Subjects.handling_command)]
    public class when_handling_command_and_an_exception_occurs_during_handling : given.a_command_coordinator
    {
        static CommandResult Result;
        static ArgumentException Exception;

        Establish context = () =>
                                {
                                    var validation_results = new List<ValidationResult>(); //no validation errors
                                    command_validation_service_mock.Setup(cvs => cvs.Validate(command_mock.Object)).Returns(validation_results);
                                };

        Because of = () =>
                         {
                            Exception = new ArgumentException();
                             command_handler_manager_mock.Setup(c => c.Handle(Moq.It.IsAny<ICommand>())).Callback(
                                 () =>
                                     {
                                         throw Exception;
                                     });

                             Result = coordinator.Handle(command_mock.Object);
                         };

        It should_have_validated_the_command = () => command_validation_service_mock.VerifyAll();
        It should_have_exception_in_result = () => Result.Exception.ShouldEqual(Exception);
        It should_have_success_set_to_false = () => Result.Success.ShouldBeFalse();
    }
}
