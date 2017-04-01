using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatParser
{
    public class when_parsing_with_one_fixed_segment : given.a_string_format_parser
    {
        static string fixed_segment = "FixedSegment";
        static IStringFormat format;

        Because of = () => format = parser.Parse($"[.]{fixed_segment}");

        It should_hold_only_one_segment = () => format.Segments.Count().ShouldEqual(1);
        It should_hold_a_fixed_string_segment = () => format.Segments.First().ShouldBeOfExactType<FixedStringSegment>();
        It should_pass_string_along_as_value = () => ((FixedStringSegment) format.Segments.First()).String.ShouldEqual(fixed_segment);
        It should_not_be_optional = () => ((FixedStringSegment) format.Segments.First()).Optional.ShouldBeFalse();
        It should_be_single_occurrence = () => ((FixedStringSegment) format.Segments.First()).Occurrences.ShouldEqual(SegmentOccurrence.Single);
    }
}
