using Machine.Specifications;
using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilderExtensions
{
    public class when_specifying_parameters : given.an_empty_command_builder
    {
        Because of = () => builder.WithParameters("Something");

        It should_set_the_parameters_on_the_builder = () => ShouldExtensionMethods.ShouldEqual(builder.Parameters,"Something");
    }
}
