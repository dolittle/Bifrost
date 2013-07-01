using Machine.Specifications;

namespace Bifrost.Utils.for_StringMapping   
{
    public class when_matching_a_string_with_wildcard_at_the_end
    {
        static StringMapping mapping = new StringMapping(
                "{something}/**/",
                "{something}.**."
            );

        static bool result;

        Because of = () => result = mapping.Matches("this/is/a/wildcard/string");

        It should_match = () => result.ShouldBeTrue();
    }
}
