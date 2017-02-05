using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatBuilder.given
{
    public class an_empty_string_format_builder
    {
        protected static StringFormatBuilder builder;

        Establish context = () => builder = new StringFormatBuilder();
    }
}
