using System.Collections.Generic;
using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using System.ComponentModel.DataAnnotations;

namespace Bifrost.Specs.Commands.for_CommandCoordinator
{
    [Subject(Subjects.handling_command)]
    public class when_handling_command_with_success : given.a_command_coordinator
    {
        static CommandResult Result;
 
        Establish context = () =>
                                {
                                    var validation_results = new List<ValidationResult>(); //no validation errors
                                    command_validation_service_mock.Setup(cvs => cvs.Validate(command_mock.Object)).Returns(validation_results);
                                };

        Because of = () =>
                         {
                             Result = coordinator.Handle(command_mock.Object);
                         };

        It should_have_validated_the_command = () => command_validation_service_mock.VerifyAll();
        It should_have_a_result = () => Result.ShouldNotBeNull();
        It should_have_success_set_to_true = () => Result.Success.ShouldBeTrue();
    }
}