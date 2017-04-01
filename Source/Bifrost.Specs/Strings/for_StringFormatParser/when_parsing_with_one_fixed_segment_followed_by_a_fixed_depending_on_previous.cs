using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatParser
{
    public class when_parsing_with_one_fixed_segment_followed_by_a_fixed_depending_on_previous : given.a_string_format_parser
    {
        static string first_fixed_segment = "FirstFixedSegment";
        static string second_fixed_segment = "SecondFixedSegment";
        static IStringFormat format;

        Because of = () => format = parser.Parse($"[.]{first_fixed_segment}.^{second_fixed_segment}");

        It should_hold_only_one_segment = () => format.Segments.Count().ShouldEqual(1);
        It should_have_a_child_segment_to_the_first = () => format.Segments.First().Children.Count().ShouldEqual(1);
        It should_hold_a_fixed_string_segment = () => format.Segments.First().ShouldBeOfExactType<FixedStringSegment>();
        It should_hold_a_fixed_string_segment_as_child = () => format.Segments.First().Children.First().ShouldBeOfExactType<FixedStringSegment>();
        It should_pass_first_string_along_as_value_for_first_segment = () => ((FixedStringSegment) format.Segments.First()).String.ShouldEqual(first_fixed_segment);
        It should_pass_second_string_along_as_value_for_child_segment = () => ((FixedStringSegment) format.Segments.First().Children.First()).String.ShouldEqual(second_fixed_segment);
        It should_consider_first_segment_not_to_be_optional = () => ((FixedStringSegment) format.Segments.First()).Optional.ShouldBeFalse();
        It should_consider_child_segment_not_to_be_optional = () => ((FixedStringSegment) format.Segments.First().Children.First()).Optional.ShouldBeFalse();
    }
}
