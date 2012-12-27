using Machine.Specifications;
using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilderExtensions
{
    public class when_specifying_type : given.an_empty_command_builder
    {
        Because of = () => builder.WithType(typeof(KnownCommand));

        It should_set_the_type_on_the_builder = () => builder.Type.ShouldEqual(typeof(KnownCommand));
    }
}
