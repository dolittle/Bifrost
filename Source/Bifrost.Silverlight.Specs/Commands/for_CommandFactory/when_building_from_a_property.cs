using Machine.Specifications;
using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandFactory
{
    public class when_building_from_a_property : given.a_command_factory
    {
        static ViewModelWithCommandsOnly view_model;
        static ICommandBuilder<ICommand> builder;
        static bool name_convention_called;

        Establish context = () => 
        {
            view_model = new ViewModelWithCommandsOnly();
            command_building_conventions.SetupGet(c=>c.CommandName).Returns(s=> {
                name_convention_called = true;
                return s;
            });
        };

        Because of = () => builder = command_factory.BuildFrom(() => view_model.TestCommand);

        It should_create_a_builder = () => builder.ShouldNotBeNull();
        It should_set_name_same_as_property = () => builder.Name.ShouldEqual("TestCommand");
        It should_call_the_naming_convention_to_resolve_name = () => name_convention_called.ShouldBeTrue();
    }
}
