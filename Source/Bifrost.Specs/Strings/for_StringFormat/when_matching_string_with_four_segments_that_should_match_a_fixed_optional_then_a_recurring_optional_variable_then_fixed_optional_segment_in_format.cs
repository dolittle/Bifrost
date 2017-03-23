using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormat
{
    public class when_matching_string_with_four_segments_that_should_match_a_fixed_optional_then_recurring_optional_variable_then_fixed_optional_segment_in_format
    {
        const string first_fixed_string = "FirstFixed";
        const string last_fixed_string = "LastFixed";
        const string variable_string = "Variable";
        const string first_segment = "FirstSegment";
        const string second_segment = "SecondSegment";
        static string full_string = $"{first_fixed_string}.{first_segment}.{second_segment}.{last_fixed_string}";
        static StringFormat string_format;
        static ISegmentMatches matches;

        Establish context = () => 
            string_format = new StringFormat(
                new ISegment[] {
                    new FixedStringSegment(
                        first_fixed_string,
                        true,
                        SegmentOccurrence.Single,
                        new NullSegment(),
                        new ISegment[0]
                    ),
                    new VariableStringSegment(
                        variable_string,
                        true,
                        SegmentOccurrence.Recurring,
                        new NullSegment(),
                        new ISegment[0]),
                    new FixedStringSegment(
                        last_fixed_string,
                        true,
                        SegmentOccurrence.Single,
                        new NullSegment(),
                        new ISegment[0]
                    ),

                }, '.');

        Because of = () => matches = string_format.Match(full_string);

        It should_have_three_matches = () => matches.Count().ShouldEqual(3);
        It should_hold_a_segment_match_with_first_fixed_string_segment = () => matches.ToArray()[0].Values.ToArray()[0].ShouldEqual(first_fixed_string);
        It should_hold_a_segment_match_with_first_variable_string_segment = () => matches.ToArray()[1].Values.ToArray()[0].ShouldEqual(first_segment);
        It should_hold_a_segment_match_with_second_variable_string_segment = () => matches.ToArray()[1].Values.ToArray()[1].ShouldEqual(second_segment);
        It should_hold_a_segment_match_with_last_fixed_string_segment = () => matches.ToArray()[2].Values.ToArray()[0].ShouldEqual(last_fixed_string);
    }
}
