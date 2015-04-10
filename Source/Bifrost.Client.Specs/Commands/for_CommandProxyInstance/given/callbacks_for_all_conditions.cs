using Bifrost.Commands;
using Machine.Specifications;
using Moq;

namespace Bifrost.Client.Specs.Commands.for_CommandProxyInstance.given
{
    public class callbacks_for_all_conditions
    {
        protected static Mock<ICommand> command_mock;

        protected static bool succeeded_called;
        protected static bool failed_called;
        protected static bool handled_called;

        protected static ICommand command_for_succeeded_callback;
        protected static ICommand command_for_failed_callback;
        protected static ICommand command_for_handled_callback;

        protected static CommandResult command_result_for_succeeded_callback;
        protected static CommandResult command_result_for_failed_callback;
        protected static CommandResult command_result_for_handled_callback;

        protected static CommandSucceeded succeeded;
        protected static CommandFailed failed;
        protected static CommandHandled handled;

        protected static CommandProxyInstance processor;

        Establish context = () =>
        {
            command_mock = new Mock<ICommand>();

            succeeded_called = false;
            failed_called = false;
            handled_called = false;

            succeeded = (c, r) => { succeeded_called = true; command_for_succeeded_callback = c; command_result_for_succeeded_callback = r; };
            failed = (c, r) => { failed_called = true; command_for_failed_callback = c; command_result_for_failed_callback = r; };
            handled = (c, r) => { handled_called = true; command_for_handled_callback = c; command_result_for_handled_callback = r; };

            processor = new CommandProxyInstance();
            processor.AddSucceeded(succeeded);
            processor.AddFailed(failed);
            processor.AddHandled(handled);
        };
    }
}
