using Machine.Specifications;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_of_command_with_constructor_parameters_set : given.an_empty_command_builder_for_known_command_with_constructor_parameter
    {
        static KnownCommandWithConstructorParameter command;

        Establish context = () => builder.ConstructorParameters = new[] { "hello" }; 

        Because of = () => command = builder.GetInstance();

        It should_create_an_instance = () => command.ShouldNotBeNull();
        It should_pass_the_parameter_along = () => command.Something.ShouldEqual("hello");
    }
}
