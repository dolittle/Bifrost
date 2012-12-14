using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandFactory
{
    public class when_building_from_a_property_with_custom_conventions : given.a_command_factory
    {
        static ViewModelWithCommandsOnly view_model;
        static ICommandBuilder builder;
        static CommandBuildingConventions custom_conventions;

        Establish context = () =>
        {
            view_model = new ViewModelWithCommandsOnly();

            custom_conventions = new CommandBuildingConventions
            {
                CommandName = (string s) => 
                {
                    return "Transformed";
                }
            };
        };

        Because of = () => builder = command_factory.BuildFrom(() => view_model.TestCommand, custom_conventions);

        It should_set_name_transformed_from_convention = () => builder.Name.ShouldEqual("Transformed");
    }
}
