using System;
using Bifrost.Web.Commands;
using Machine.Specifications;

namespace Bifrost.Web.Specs.Commands.for_CommandContextConnectionManager
{
    public class when_getting_a_connection_from_a_non_registered_command_context
    {
        static CommandContextConnectionManager manager;
        static Exception exception;

        Establish context = () => manager = new CommandContextConnectionManager();

        Because of = () => exception = Catch.Exception(() => manager.GetConnectionForCommandContext(Guid.NewGuid()));

        It should_throw_unknown_command_context_exception = () => exception.ShouldBeOfExactType<UnknownCommandContextException>();
    }
}
