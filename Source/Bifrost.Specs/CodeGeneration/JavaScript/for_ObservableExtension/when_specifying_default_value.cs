using Bifrost.CodeGeneration.JavaScript;
using Machine.Specifications;

namespace Bifrost.Specs.CodeGeneration.JavaScript.for_ObservableExtension
{
    public class when_specifying_default_value : given.an_observable_without_default_value
    {
        Because of = () => observable.WithDefaultValue("Something");

        It should_have_one_parameter = () => observable.Parameters.Length.ShouldEqual(1);
        It should_have_one_literal_parameter = () => observable.Parameters[0].ShouldBeOfExactType<Literal>();
        It should_have_the_default_parameter_as_a_parameter = () => ((Literal)observable.Parameters[0]).Content.ShouldEqual("Something");
    }
}
