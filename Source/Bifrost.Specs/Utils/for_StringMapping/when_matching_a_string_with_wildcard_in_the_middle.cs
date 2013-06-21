using Machine.Specifications;

namespace Bifrost.Utils.for_StringMapping   
{
    public class when_matching_a_string_with_wildcard_in_the_middle
    {
        static StringMapping mapping = new StringMapping(
                "{something}/**/for_{else}",
                "{something}.**.for_{else}"
            );

        static bool result;

        Because of = () => result = mapping.Matches("this/is/a/wildcard/string/for_things");

        It should_match = () => result.ShouldBeTrue();
    }
}
