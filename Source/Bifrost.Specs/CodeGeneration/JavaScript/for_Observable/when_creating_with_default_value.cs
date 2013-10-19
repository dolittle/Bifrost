using Bifrost.CodeGeneration.JavaScript;
using Machine.Specifications;

namespace Bifrost.Specs.CodeGeneration.JavaScript.for_Observable
{
    public class when_creating_without_default_value
    {
        static Observable observable;

        Because of = () => observable = new Observable();

        It should_set_function_to_be_a_knockout_observable = () => observable.Function.ShouldEqual("ko.observable");
        It should_not_have_any_parameters = () => observable.Parameters.ShouldBeNull();
    }
}
