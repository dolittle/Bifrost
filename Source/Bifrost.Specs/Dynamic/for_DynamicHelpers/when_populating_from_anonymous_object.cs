using System.Dynamic;
using Machine.Specifications;
using Bifrost.Dynamic;

namespace Bifrost.Specs.Extensions.for_DynamicHelpers
{
    public class when_populating_from_anonymous_object
    {
        static dynamic  target;
        static object source; 

        Establish context = () => {
            target = new ExpandoObject();
            source = new {
                SomeDouble = 0.5,
                SomeInt = 42,
                SomeString = "Something"
            };
        };

        Because of = () => DynamicHelpers.Populate(target,source);

        It should_have_double_value_on_it = () => ShouldExtensionMethods.ShouldEqual(target.SomeDouble, 0.5);
        It should_have_int_value_on_it = () => ShouldExtensionMethods.ShouldEqual(target.SomeInt,42);
        It should_have_string_value_on_it = () => ShouldExtensionMethods.ShouldEqual(target.SomeString,"Something");
    }
}
