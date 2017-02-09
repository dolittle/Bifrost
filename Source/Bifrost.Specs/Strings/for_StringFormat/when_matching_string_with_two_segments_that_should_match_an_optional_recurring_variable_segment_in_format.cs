using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormat
{
    public class when_matching_string_with_two_segments_that_should_match_an_optional_recurring_variable_segment_in_format
    {
        const string variable_string = "Variable";
        const string first_segment = "FirstSegment";
        const string second_segment = "SecondSegment";
        static string full_string = $"{first_segment}.{second_segment}";
        static StringFormat string_format;
        static ISegmentMatches matches;

        Establish context = () => 
            string_format = new StringFormat(
                new ISegment[] {
                    new VariableStringSegment(
                        variable_string,
                        true,
                        SegmentOccurence.Recurring,
                        new NullSegment(),
                        new ISegment[0])
                }, '.');

        Because of = () => matches = string_format.Match(full_string);

        It should_have_one_match = () => matches.Count().ShouldEqual(1);
        It should_have_two_values = () => matches.First().Values.Count().ShouldEqual(2);
        It should_hold_a_segment_match_with_first_segment = () => matches.First().Values.ToArray()[0].ShouldEqual(first_segment);
        It should_hold_a_segment_match_with_second_segment = () => matches.First().Values.ToArray()[1].ShouldEqual(second_segment);
    }
}
