using Bifrost.Utils;
using Machine.Specifications;

namespace Bifrost.Specs.Utils.for_StringMapping
{
    public class when_matching_a_string_with_only_a_wildcard
    {
        static StringMapping mapping = new StringMapping(
                "**/",
                "**."
            );
        static bool result;

        Because of = () => result = mapping.Matches("this/is/a/wildcard/uri/for_things");

        It should_match = () => result.ShouldBeTrue();
    }
}
