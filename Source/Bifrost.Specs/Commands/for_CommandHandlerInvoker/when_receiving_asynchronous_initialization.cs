using System;
using System.Threading;
using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandHandlerInvoker
{
    [Subject(Subjects.handling_commands)]
    public class when_receiving_asynchronous_initialization : given.a_command_handler_invoker_with_no_command_handlers
    {
        protected static bool result;

        Because of = () =>
        {
            var command = new Command();
            var thread = new Thread(() => invoker.TryHandle(command));

            type_discoverer_mock
                .Setup(t => t.FindMultiple<IHandleCommands>())
                .Callback(
                    () =>
                    {
                        thread.Start();
                        Thread.Sleep(50);
                    })
                .Returns(new Type[0]);
            result = invoker.TryHandle(command);
            thread.Join();
        };

        It should_initialize_only_once = () =>
            type_discoverer_mock.Verify(m => m.FindMultiple<IHandleCommands>(), Times.Once);
    }
}
