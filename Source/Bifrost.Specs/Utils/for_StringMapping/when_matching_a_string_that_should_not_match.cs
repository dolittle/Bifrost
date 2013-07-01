using Bifrost.Utils;
using Machine.Specifications;

namespace Bifrost.Specs.Utils.for_StringMapping
{
    public class when_matching_a_string_that_should_not_match
    {
        static StringMapping mapping = new StringMapping(
                "{something}/{else}",
                "whatevva"
            );
        static bool result;

        Because of = () => result = mapping.Matches("hello");

        It should_not_match = () => result.ShouldBeFalse();
    }
}
