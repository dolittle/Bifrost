using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatParser
{
    public class when_parsing_with_one_variable_segment : given.a_string_format_parser
    {
        static string variable_segment = "VariableSegment";
        static IStringFormat format;

        Because of = () => format = parser.Parse($"[.]{{{variable_segment}}}");

        It should_hold_only_one_segment = () => format.Segments.Count().ShouldEqual(1);
        It should_hold_a_variable_string_segment = () => format.Segments.First().ShouldBeOfExactType<VariableStringSegment>();
        It should_pass_string_along_as_value = () => ((VariableStringSegment) format.Segments.First()).VariableName.ShouldEqual(variable_segment);
        It should_not_be_optional = () => ((VariableStringSegment) format.Segments.First()).Optional.ShouldBeFalse();
        It should_be_single_occurrence = () => ((VariableStringSegment) format.Segments.First()).Occurrences.ShouldEqual(SegmentOccurrence.Single);
    }
}
