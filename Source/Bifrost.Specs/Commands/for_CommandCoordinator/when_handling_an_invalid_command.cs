using System.ComponentModel.DataAnnotations;
using Bifrost.Commands;
using Machine.Specifications;
using Bifrost.Validation;

namespace Bifrost.Specs.Commands.for_CommandCoordinator
{
    [Subject(typeof(CommandCoordinator))]
    public class when_handling_an_invalid_command : given.a_command_coordinator
    {
        static CommandResult Result;
        static CommandValidationResult validation_errors;

    	Establish context = () =>
    	                    	{
                                    validation_errors = new CommandValidationResult
                                    {
                                        ValidationResults = new ValidationResult[] 
    	                    		                    	{
    	                    		                    		new ValidationResult("First validation failure"),
    	                    		                    		new ValidationResult("Second validation failure")
    	                    		                    	}
                                    };

    	                    		command_validators_mock.Setup(cvs => cvs.Validate(command_mock.Object)).Returns(
    	                    			validation_errors);
    	                    	};

    	Because of = () => Result = coordinator.Handle(command_mock.Object);
    	             	

        It should_have_validated_the_command = () => command_validators_mock.VerifyAll();
        It should_have_a_result = () => Result.ShouldNotBeNull();
        It should_have_success_set_to_false = () => Result.Success.ShouldBeFalse();
        It should_have_a_record_of_each_validation_failure = () => Result.ValidationResults.ShouldContainOnly(validation_errors.ValidationResults);
        It should_not_handle_the_command = () => command_handler_manager_mock.Verify(chm => chm.Handle(command_mock.Object), Moq.Times.Never());
        It should_rollback_the_command_context = () => command_context_mock.Verify(c => c.Rollback(), Moq.Times.Once());
    }
}