using Machine.Specifications;
using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilderExtensions
{
    public class when_specifying_constructor_parameters : given.an_empty_command_builder
    {
        Because of = () => builder.WithConstructorParameters("Something","Else");

        It should_set_the_constructor_parameters_on_the_builder = () => builder.ConstructorParameters.Length.ShouldEqual(2);
    }
}
