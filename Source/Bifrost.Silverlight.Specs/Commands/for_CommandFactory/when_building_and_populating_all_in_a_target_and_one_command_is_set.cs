using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandFactory
{
    public class when_building_and_populating_all_in_a_target_and_one_command_is_set : given.a_command_factory
    {
        static ViewModelWithCommandsOnly view_model;
        static ICommand existing_command;

        Establish context = () =>
        {
            view_model = new ViewModelWithCommandsOnly();
            existing_command = new Mock<ICommand>().Object;
            view_model.TestCommand = existing_command;
        };

        Because of = () => command_factory.BuildAndPopulateAll(view_model);

        It should_not_create_a_new_instance_for_the_one_already_set = () => view_model.TestCommand.ShouldEqual(existing_command);
    }
}
