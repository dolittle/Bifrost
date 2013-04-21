using Machine.Specifications;
using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandFactory
{
    public class when_building_and_populating_all_in_a_target_with_custom_conventions : given.a_command_factory
    {
        static ViewModelWithCommandsOnly view_model;
        static CommandBuildingConventions custom_conventions;

        Establish context = () => 
        {
            view_model = new ViewModelWithCommandsOnly();
            custom_conventions = new CommandBuildingConventions
            {
                CommandName = (string s) =>
                {
                    return s+"Transformed";
                }
            };
        };

        Because of = () => command_factory.BuildAndPopulateAll(view_model, custom_conventions);

        It should_create_an_instance_of_test_command = () => view_model.TestCommand.ShouldNotBeNull();
        It should_create_an_instance_of_second_command = () => view_model.SecondCommand.ShouldNotBeNull();

        It should_set_name_transformed_from_convention_for_test_command = () => view_model.TestCommand.Name.ShouldEqual("TestCommandTransformed");
        It should_set_name_transformed_from_convention_for_second_command = () => view_model.SecondCommand.Name.ShouldEqual("SecondCommandTransformed");
    }
}
