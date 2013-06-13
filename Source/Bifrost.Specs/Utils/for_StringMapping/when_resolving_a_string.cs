using Bifrost.Utils;
using Machine.Specifications;
namespace Bifrost.Specs.Utils.for_StringMapping
{
    public class when_resolving_a_string
    {
        const string expected_result = "Say/hello/to/mr.potatohead";
        static StringMapping    mapping;
        static string result;

        Establish context = () => mapping = new StringMapping(
                "{something}/{else}",
                "Say/{else}/to/{something}"
            );

        Because of = () => result = mapping.Resolve("mr.potatohead/hello");

        It should_expand_input_string_to_mapped_string = () => result.ShouldEqual(expected_result);
    }
}
