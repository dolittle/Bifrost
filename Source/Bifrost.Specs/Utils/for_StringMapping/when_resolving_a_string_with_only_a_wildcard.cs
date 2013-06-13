using Bifrost.Utils;
using Machine.Specifications;

namespace Bifrost.Specs.Utils.for_StringMapping
{
    public class when_resolving_a_string_with_only_a_wildcard
    {
        const string expected_result = "this.is.a.wildcard.uri.for_things";

        static StringMapping mapping = new StringMapping(
                "**/",
                "**."
            );
        static string result;

        Because of = () => result = mapping.Resolve("this/is/a/wildcard/uri/for_things");

        It should_expand_uri_to_the_mapped_uri = () => result.ShouldEqual(expected_result);
    }
}
