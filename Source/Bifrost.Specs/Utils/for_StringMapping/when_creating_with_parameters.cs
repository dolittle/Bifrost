using Bifrost.Utils;
using Machine.Specifications;

namespace Bifrost.Specs.Utils.for_StringMapping
{
    public class when_creating_with_parameters
    {
        static StringMapping mapping;

        Because of = () => mapping = new StringMapping("hello", "world");

        It should_have_the_format_on_it = () => mapping.Format.ShouldEqual("hello");
        It should_have_the_mapped_format_on_it = () => mapping.MappedFormat.ShouldEqual("world");
    }
}
