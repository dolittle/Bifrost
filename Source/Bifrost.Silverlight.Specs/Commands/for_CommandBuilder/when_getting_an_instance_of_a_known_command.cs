using Machine.Specifications;
using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_of_a_known_command : given.an_empty_command_builder
    {
        static ICommand command;

        Establish context = () =>
        {
            builder.Name = typeof(KnownCommand).Name;
            builder.Type = typeof(KnownCommand);
        };

        Because of = () => command = builder.GetInstance();

        It should_return_an_instance = () => command.ShouldNotBeNull();
    }
}
