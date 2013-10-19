using System.Linq;
using Bifrost.CodeGeneration.JavaScript;
using Machine.Specifications;

namespace Bifrost.Specs.CodeGeneration.JavaScript.for_ObservableExtension
{
    public class when_extending : given.an_observable_without_default_value
    {
        const string extension_name = "Something";
        const string extension_options = "{ blah : { asdasd:'asdasd'} }";

        Because of = () => observable.ExtendWith(extension_name, extension_options);

        It should_add_the_extension = () => observable.Extensions.Count().ShouldEqual(1);
        It should_pass_the_name_to_the_extension = () => observable.Extensions.First().Name.ShouldEqual(extension_name);
        It should_pass_the_options_to_the_extension = () => observable.Extensions.First().Options.ShouldEqual(extension_options);
    }
}
