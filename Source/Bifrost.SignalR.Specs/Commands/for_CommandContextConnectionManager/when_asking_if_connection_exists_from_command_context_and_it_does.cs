using System;
using Bifrost.SignalR.Commands;
using Machine.Specifications;

namespace Bifrost.SignalR.Specs.Commands.for_CommandContextConnectionManager
{
    public class when_asking_if_connection_exists_from_command_context_and_it_does
    {
        static Guid command_context = Guid.NewGuid();

        static CommandContextConnectionManager manager;
        static bool result;
        

        Establish context = () => 
        {
            manager = new CommandContextConnectionManager();
            manager.Register("Blah", command_context);
        };

        Because of = () => result = manager.HasConnectionForCommandContext(command_context);

        It should_return_true = () => result.ShouldBeTrue();
    }
}
