using Bifrost.Commands;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Client.Specs.Commands.for_CommandProxyInstance
{
    public class when_processing_failed_command : given.callbacks_for_all_conditions
    {
        static CommandResult command_result;

        Establish context = () => command_result = new CommandResult { ValidationResults = new ValidationResult[1] };

        Because of = () => processor.Process(command_mock.Object, command_result);

        It should_call_the_failed_callback = () => failed_called.ShouldBeTrue();
        It should_not_call_the_succeeded_callback = () => succeeded_called.ShouldBeFalse();
        It should_call_the_handled_callback = () => handled_called.ShouldBeTrue();

        It should_pass_the_command_to_the_failed_callback = () => command_for_failed_callback.ShouldEqual(command_mock.Object);
        It should_pass_the_command_to_the_handled_callback = () => command_for_handled_callback.ShouldEqual(command_mock.Object);

        It should_pass_the_command_result_to_the_failed_callback = () => command_result_for_failed_callback.ShouldEqual(command_result);
        It should_pass_the_command_result_to_the_handled_callback = () => command_result_for_handled_callback.ShouldEqual(command_result);
    }
}
