using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_and_type_is_specified : given.an_empty_command_builder
    {
        static ICommand command;

        Establish context = () =>
        {
            conventions_mock.Setup(c => c.CommandName).Returns(n => n);
            builder.Type = typeof(KnownCommand);
        };

        Because of = () => command = builder.GetInstance();

        It should_create_correct_command_instance = () => command.ShouldBeOfType<KnownCommand>();
    }
}
