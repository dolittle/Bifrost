using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormat
{
    public class when_matching_string_with_three_segments_that_should_match_a_fixed_non_optional_then_single_non_optional_then_fixed_non_optional_and_then_two_optional_variables
    {
        const string first_fixed_string = "FirstFixed";
        const string second_fixed_string = "SecondFixed";
        const string first_variable_string = "FirstVariable";
        const string second_variable_string = "SecondVariable";
        const string third_variable_string = "ThirdVariable";
        const string first_segment = "FirstSegment";
        static string full_string = $"{first_fixed_string}.{first_segment}.{second_fixed_string}";
        static StringFormat string_format;
        static ISegmentMatches matches;

        //.Events("Oculos.{BoundedContext}.Events.-{Module}.-{Feature}.^{SubFeature}*")

        Establish context = () =>
        {
            var firstSegment = new FixedStringSegment(
                        first_fixed_string,
                        false,
                        SegmentOccurrence.Single,
                        new NullSegment(),
                        new ISegment[0]
                    );
            var secondSegment = new VariableStringSegment(
                        first_variable_string,
                        false,
                        SegmentOccurrence.Single,
                        firstSegment,
                        new ISegment[0]);
            var thirdSegment = new FixedStringSegment(
                        second_fixed_string,
                        false,
                        SegmentOccurrence.Single,
                        secondSegment,
                        new ISegment[0]);

            var fourthSegment = new VariableStringSegment(
                        second_variable_string,
                        true,
                        SegmentOccurrence.Single,
                        thirdSegment,
                        new ISegment[0]);

            var fifthSegment = new VariableStringSegment(
                        third_variable_string,
                        true,
                        SegmentOccurrence.Recurring,
                        fourthSegment,
                        new ISegment[0]);

            string_format = new StringFormat(
                new ISegment[] {
                    firstSegment, secondSegment, thirdSegment, fourthSegment, fifthSegment
                }, '.');
        };

        Because of = () => matches = string_format.Match(full_string);

        It should_have_three_matches = () => matches.Count().ShouldEqual(3);
        It should_hold_a_segment_match_with_first_fixed_string_segment = () => matches.ToArray()[0].Values.ToArray()[0].ShouldEqual(first_fixed_string);
        It should_hold_a_segment_match_with_first_variable_string_segment = () => matches.ToArray()[1].Values.ToArray()[0].ShouldEqual(first_segment);
        It should_hold_a_segment_match_with_second_fixed_string_segment = () => matches.ToArray()[2].Values.ToArray()[0].ShouldEqual(second_fixed_string);
    }
}
