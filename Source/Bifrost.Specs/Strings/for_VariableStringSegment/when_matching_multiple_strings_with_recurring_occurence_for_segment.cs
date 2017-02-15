using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_VariableStringSegment
{
    public class when_matching_multiple_strings_with_recurring_occurence_for_segment
    {
        const string variable_name = "Variable";
        const string first_matching_string = "Something";
        const string second_matching_string = "Something Else";

        static VariableStringSegment segment;
        static ISegmentMatch result;

        Establish context = () => segment = new VariableStringSegment(variable_name, false, SegmentOccurrence.Recurring, new NullSegment(), new ISegment[0]);

        Because of = () => result = segment.Match(new[] { first_matching_string, second_matching_string });

        It should_consider_it_a_match = () => result.HasMatch.ShouldBeTrue();
        It should_hold_the_variable_name_as_identifier = () => result.Identifier.ShouldEqual(variable_name);
        It should_have_two_matching_values = () => result.Values.Count().ShouldEqual(2);
        It should_have_the_first_matching_string_as_value = () => result.Values.ToArray()[0].ShouldEqual(first_matching_string);
        It should_have_the_second_matching_string_as_value = () => result.Values.ToArray()[1].ShouldEqual(second_matching_string);
        It should_have_segment_as_source = () => result.Source.ShouldEqual(segment);
    }
}
