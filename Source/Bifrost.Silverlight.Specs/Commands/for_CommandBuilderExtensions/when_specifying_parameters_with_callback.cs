using Machine.Specifications;
using Bifrost.Commands;
using System.Dynamic;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilderExtensions
{
    public class when_specifying_parameters_with_callback : given.an_empty_command_builder
    {
        static dynamic parameters = null;

        Because of = () => builder.WithParameters(p => parameters = p);

        It call_the_callback_with_an_expando_object = () => ShouldExtensionMethods.ShouldBeOfType<ExpandoObject>(parameters);
    }
}
