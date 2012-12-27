using Machine.Specifications;
using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandFactory
{
    public class when_building_from_a_known_type : given.a_command_factory
    {
        static ICommandBuilder<KnownCommand> builder;

        Establish context = () => { };

        Because of = () => builder = command_factory.BuildFor<KnownCommand>();

        It should_return_a_builder = () => builder.ShouldNotBeNull();
        It should_hold_the_name_of_the_type = () => builder.Name.ShouldEqual(typeof(KnownCommand).Name);
    }
}
