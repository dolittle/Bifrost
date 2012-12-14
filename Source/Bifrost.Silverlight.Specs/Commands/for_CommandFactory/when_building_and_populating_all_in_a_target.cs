using Machine.Specifications;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandFactory
{
    public class when_building_and_populating_all_in_a_target : given.a_command_factory
    {
        static ViewModelWithCommandsOnly view_model;
        static bool name_convention_called;

        Establish context = () => 
        {
            view_model = new ViewModelWithCommandsOnly();
            command_building_conventions.SetupGet(c => c.CommandName).Returns(s =>
            {
                name_convention_called = true;
                return s;
            });
        };

        Because of = () => command_factory.BuildAndPopulateAll(view_model);

        It should_create_an_instance_of_test_command = () => view_model.TestCommand.ShouldNotBeNull();
        It should_create_an_instance_of_second_command = () => view_model.SecondCommand.ShouldNotBeNull();

        It should_set_name_same_as_property_for_test_command = () => view_model.TestCommand.Name.ShouldEqual("TestCommand");
        It should_set_name_same_as_property_for_second_command = () => view_model.SecondCommand.Name.ShouldEqual("SecondCommand");

        It should_call_the_naming_convention_to_resolve_name = () => name_convention_called.ShouldBeTrue();
    }
}
