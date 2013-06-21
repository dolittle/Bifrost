using Machine.Specifications;

namespace Bifrost.Utils.for_StringMapping   
{
    public class when_resolving_a_string_with_wildcard_at_the_end
    {
        const string expected_result = "this.is.a.wildcard.string";

        static StringMapping mapping = new StringMapping(
                "{something}/**/",
                "{something}.**."
            );

        static string result;

        Because of = () => result = mapping.Resolve("this/is/a/wildcard/string");

        It should_expand_input_string_to_the_mapped_string = () => result.ShouldEqual(expected_result);
    }
}
