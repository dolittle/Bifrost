using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatParser.given
{
    public class a_string_format_parser
    {
        protected static StringFormatParser parser;

        Establish context = () => parser = new StringFormatParser();
    }
}
