using Bifrost.Utils;
using Machine.Specifications;

namespace Bifrost.Specs.Utils.for_StringMapping
{
    public class when_resolving_a_string_with_wildcard_in_the_middle
    {
        const string expected_result = "this.is.a.wildcard.string.for_things";

        static StringMapping mapping = new StringMapping(
                "{something}/**/for_{else}",
                "{something}.**.for_{else}"
            );

        static string result;

        Because of = () => result = mapping.Resolve("this/is/a/wildcard/string/for_things");

        It should_expand_input_string_to_the_mapped_string = () => result.ShouldEqual(expected_result);
    }
}
