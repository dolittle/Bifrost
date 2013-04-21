using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_with_name : given.an_empty_command_builder
    {
        static ICommand result;

        Establish context = () => builder.Name = "Test";

        Because of = () => result = builder.GetInstance();

        It should_return_a_command_instance = () => result.ShouldNotBeNull();
        It should_set_the_name_on_the_command = () => result.Name.ShouldEqual(builder.Name);
    }
}
