using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_FixedStringSegment
{
    public class when_matching_multiple_matchable_string_that_only_has_one_occurrence_segment
    {
        const string string_to_match = "TheString";
        static FixedStringSegment segment;
        static ISegmentMatch result;

        Establish context = () => segment = new FixedStringSegment(string_to_match, false, SegmentOccurrence.Single, new NullSegment(), new ISegment[0]);

        Because of = () => result = segment.Match(new[] { string_to_match, string_to_match });

        It should_consider_it_a_match = () => result.HasMatch.ShouldBeTrue();
        It should_refer_to_segment_matched = () => result.Source.ShouldEqual(segment);
        It should_only_have_one_value = () => result.Values.Count().ShouldEqual(1);
        It should_hold_the_string_to_match_as_only_value = () => result.Values.Single().ShouldEqual(string_to_match);
    }
}
