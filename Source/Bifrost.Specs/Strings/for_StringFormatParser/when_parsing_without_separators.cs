using System;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatParser
{
    public class when_parsing_without_separators : given.a_string_format_parser
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => parser.Parse(string.Empty));

        It should_throw_missing_separators_in_format_string = () => exception.ShouldBeOfExactType<MissingSeparatorsInFormatString>();
    }
}
