using System;
using Bifrost.Commands;
using Bifrost.Testing.Fakes.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandContextFactory
{
    [Subject(typeof (CommandContextFactory))]
    public class when_building_for_a_command : given.a_command_context_factory
    {
        static ICommand command;
        static ICommandContext command_context;

        Establish context = () =>
            {
                command = new SimpleCommand();
            };

        Because of = () => command_context = factory.Build(command);

        It should_build_a_commmand_context = () => command_context.ShouldBeOfType<CommandContext>();
        It should_contain_the_commmand = () => command_context.Command.ShouldEqual(command);
    }
}