using Bifrost.CodeGeneration.JavaScript;
using Machine.Specifications;
using System.Linq;

namespace Bifrost.Specs.CodeGeneration.JavaScript.for_Observable
{
    public class when_adding_extension
    {
        const string extension_name = "Something";
        const string extension_options = "{ blah : { asdasd:'asdasd'} }";

        static Observable observable;

        Establish context = () => observable = new Observable();

        Because of = () => observable.AddExtension(extension_name, extension_options);

        It should_add_the_extension = () => observable.Extensions.Count().ShouldEqual(1);
        It should_pass_the_name_to_the_extension = () => observable.Extensions.First().Name.ShouldEqual(extension_name);
        It should_pass_the_options_to_the_extension = () => observable.Extensions.First().Options.ShouldEqual(extension_options);
    }
}
