using Machine.Specifications;
using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilderExtensions
{
    public class when_specifying_name : given.an_empty_command_builder
    {
        Because of = () => builder.WithName("Test");

        It should_set_the_name_on_the_builder = () => builder.Name.ShouldEqual("Test");
    }
}
