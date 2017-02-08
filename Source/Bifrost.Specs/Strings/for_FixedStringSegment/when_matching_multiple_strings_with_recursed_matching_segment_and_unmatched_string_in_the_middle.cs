using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_FixedStringSegment
{
    public class when_matching_multiple_strings_with_recursed_matching_segment_and_unmatched_string_in_the_middle
    {
        const string unmatched_string = "SomethingElse";
        const string string_to_match = "TheString";
        static FixedStringSegment segment;
        static ISegmentMatch result;

        Establish context = () => segment = new FixedStringSegment(string_to_match, false, SegmentOccurence.Recurring, new NullSegment(), new ISegment[0]);

        Because of = () => result = segment.Match(new[] { string_to_match, unmatched_string, string_to_match });

        It should_consider_it_a_match = () => result.HasMatch.ShouldBeTrue();
        It should_refer_to_segment_matched = () => result.Source.ShouldEqual(segment);
        It should_have_one_value = () => result.Values.Count().ShouldEqual(1);
        It should_hold_match_in_first_value = () => result.Values.First().ShouldEqual(string_to_match);
    }
}
