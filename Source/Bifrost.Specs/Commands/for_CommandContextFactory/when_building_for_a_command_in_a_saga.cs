using Bifrost.Commands;
using Bifrost.Sagas;
using Bifrost.Testing.Fakes.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandContextFactory
{
    [Subject(typeof(CommandContextFactory))]
    public class when_building_for_a_command_in_a_saga : given.a_command_context_factory
    {
        static ICommand command;
        static ISaga saga;
        static ICommandContext command_context;

        Establish context = () =>
            {
                command = new SimpleCommand();
                saga = new Saga();
            };

        Because of = () => command_context = factory.Build(saga,command);

        It should_build_a_saga_commmand_context = () => command_context.ShouldBeOfExactType<SagaCommandContext>();
        It should_contain_the_commmand = () => command_context.Command.ShouldEqual(command);
    }
}