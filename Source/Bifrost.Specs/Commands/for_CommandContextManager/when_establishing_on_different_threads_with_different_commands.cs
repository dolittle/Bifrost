using System.Threading;
using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandContextManager
{
    [Subject(Subjects.establishing_context)]
    public class when_establishing_on_different_threads_with_different_commands : given.a_command_context_manager
    {
        static ICommandContext firstCommandContext;
        static ICommandContext secondCommandContext;

        Establish context = () =>
                                {
                                    var resetEvent = new ManualResetEvent(false);
                                    firstCommandContext = Manager.EstablishForCommand(new SimpleCommand());
                                    var thread = new Thread(
                                        () =>
                                            {
                                                secondCommandContext = Manager.EstablishForCommand(new SimpleCommand());
                                                resetEvent.Reset();
                                            }
                                        );
                                    thread.Start();
                                    resetEvent.WaitOne(1000);
                                };

        It should_return_different_contexts = () => firstCommandContext.ShouldNotEqual(secondCommandContext);
    }
}