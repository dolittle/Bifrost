using System;
using Bifrost.Web.Commands;
using Machine.Specifications;

namespace Bifrost.Web.Specs.Commands.for_CommandContextConnectionManager
{
    public class when_asking_if_connection_exists_from_command_context_and_it_does_not
    {
        static Guid command_context = Guid.NewGuid();

        static CommandContextConnectionManager manager;
        static bool result;
        

        Establish context = () => manager = new CommandContextConnectionManager();

        Because of = () => result = manager.HasConnectionForCommandContext(command_context);

        It should_return_false = () => result.ShouldBeFalse();
    }
}
