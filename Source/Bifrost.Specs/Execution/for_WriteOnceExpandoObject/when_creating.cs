using Machine.Specifications;
using Bifrost.Execution;

namespace Bifrost.Specs.Execution.for_WriteOnceExpandoObject
{
    [Subject(typeof(WriteOnceExpandoObject))]
    public class when_creating
    {
        static dynamic expando_object;
        static dynamic populated_object;
        static bool is_population_callback_called;

        Because of = () => expando_object = new WriteOnceExpandoObject(p =>
        {
            is_population_callback_called = true;
            populated_object = p;
        });

        It should_call_the_population_callback = () => is_population_callback_called.ShouldBeTrue();
        It should_call_the_population_callback_with_object_being_created = () => ((object)populated_object).ShouldEqual((object)expando_object);
    }
}
