using Bifrost.CodeGeneration.JavaScript;
using Machine.Specifications;
using System.Linq;

namespace Bifrost.Specs.CodeGeneration.JavaScript.for_Observable
{
    public class when_creating_with_default_value
    {
        static Observable observable;

        Because of = () => observable = new Observable(42);

        It should_set_function_to_be_a_knockout_observable = () => observable.Function.ShouldEqual("ko.observable");
        It should_have_a_parameter_set_to_the_default_value = () => ((Literal)observable.Parameters.First()).Content.ShouldEqual(42);
    }
}
