using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormat
{
    public class when_matching_string_with_four_segments_that_does_not_match_fixed_recurring_then_recurring_optional_variable_then_fixed_optional_segment_in_format
    {
        const string first_fixed_string = "FirstFixed";
        const string unmatched_first_fixed_string = "UnmatchedFirstFixed";
        const string last_fixed_string = "LastFixed";
        const string variable_string = "Variable";
        const string first_segment = "FirstSegment";
        const string second_segment = "SecondSegment";
        static string full_string = $"{unmatched_first_fixed_string}.{first_segment}.{second_segment}.{last_fixed_string}";
        static StringFormat string_format;
        static ISegmentMatches matches;

        Establish context = () => 
            string_format = new StringFormat(
                new ISegment[] {
                    new FixedStringSegment(
                        first_fixed_string,
                        false,
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

        It should_not_match = () => matches.HasMatches.ShouldBeFalse();
    }
}
