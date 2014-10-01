using System;
using Bifrost.Web.Commands;
using Machine.Specifications;

namespace Bifrost.Web.Specs.Commands.for_CommandContextConnectionManager
{
    public class when_removing_a_connection_registered_for_two_command_contexts
    {
        static string connection_id = Guid.NewGuid().ToString();
        static Guid first_command_context = Guid.NewGuid();
        static Guid second_command_context = Guid.NewGuid();

        static CommandContextConnectionManager manager;

        Establish context = () =>
        {
            manager = new CommandContextConnectionManager();
            manager.Register(connection_id, first_command_context);
            manager.Register(connection_id, second_command_context);
        };

        Because of = () => manager.RemoveConnectionIfPresent(connection_id);

        It should_not_have_the_first_command_context_registered = () => Catch.Exception(() => manager.GetConnectionForCommandContext(first_command_context)).ShouldBeOfType<UnknownCommandContextException>();
        It should_not_have_the_second_command_context_registered = () => Catch.Exception(() => manager.GetConnectionForCommandContext(first_command_context)).ShouldBeOfType<UnknownCommandContextException>();
    }
}
