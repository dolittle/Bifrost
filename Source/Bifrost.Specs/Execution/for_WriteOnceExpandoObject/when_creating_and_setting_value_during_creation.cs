using Machine.Specifications;
using Bifrost.Execution;

namespace Bifrost.Specs.Execution.for_WriteOnceExpandoObject
{
    [Subject(typeof(WriteOnceExpandoObject))]
    public class when_creating_and_setting_value_during_creation
    {
        static dynamic expando_object;

        Because of = () => expando_object = new WriteOnceExpandoObject(p =>
        {
            p.Something = "Hello world";
        });

        It should_have_the_value_set = () => ((string)expando_object.Something).ShouldEqual("Hello world");
    }
}
