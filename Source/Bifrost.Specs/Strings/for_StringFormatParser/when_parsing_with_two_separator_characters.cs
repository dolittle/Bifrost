using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatParser
{
    public class when_parsing_with_two_separator_characters : given.a_string_format_parser
    {
        static char[] separators = new[] { '.', ',' };
        static IStringFormat format;

        Because of = () => format = parser.Parse($"[{separators[0]}{separators[1]}]");

        It should_hold_the_separators_in_format = () => format.Separators.ShouldEqual(separators);
    }
}
