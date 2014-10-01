using System;
using Bifrost.Web.Commands;
using Machine.Specifications;

namespace Bifrost.Web.Specs.Commands.for_CommandContextConnectionManager
{
    public class when_getting_an_existing_connection_from_a_command_context
    {
        static string connection_id = Guid.NewGuid().ToString();
        static Guid command_context = Guid.NewGuid();
        static string result;

        static CommandContextConnectionManager manager;

        Establish context = () => 
        {
            manager = new CommandContextConnectionManager();
            manager.Register(connection_id, command_context);
        };

        Because of = () => result = manager.GetConnectionForCommandContext(command_context);

        It should_get_the_registered_connection = () => result.ShouldEqual(connection_id);
    }
}
