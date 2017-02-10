using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_FixedStringSegment
{
    public class when_matching_multiple_strings_with_only_second_string_matching
    {
        const string unmatched_string = "SomethingElse";
        const string string_to_match = "TheString";
        static FixedStringSegment segment;
        static ISegmentMatch result;

        Establish context = () => segment = new FixedStringSegment(string_to_match, false, SegmentOccurrence.Recurring, new NullSegment(), new ISegment[0]);

        Because of = () => result = segment.Match(new[] { unmatched_string, string_to_match });

        It should_not_consider_it_a_match = () => result.HasMatch.ShouldBeFalse();
        It should_refer_to_segment_matched = () => result.Source.ShouldEqual(segment);
        It should_not_have_any_values = () => result.Values.Count().ShouldEqual(0);
    }
}
