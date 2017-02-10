using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_VariableStringSegment
{
    public class when_matching_multiple_strings_with_only_one_occurence_for_segment
    {
        const string variable_name = "Variable";
        const string matching_string = "Something";

        static VariableStringSegment segment;
        static ISegmentMatch result;

        Establish context = () => segment = new VariableStringSegment(variable_name, false, SegmentOccurrence.Single, new NullSegment(), new ISegment[0]);

        Because of = () => result = segment.Match(new[] { matching_string, matching_string });

        It should_consider_it_a_match = () => result.HasMatch.ShouldBeTrue();
        It should_have_one_matching_value = () => result.Values.Count().ShouldEqual(1);
        It should_have_the_matching_string_as_value = () => result.Values.First().ShouldEqual(matching_string);
        It should_have_segment_as_source = () => result.Source.ShouldEqual(segment);
    }
}
