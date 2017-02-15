using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_FixedStringSegment
{
    public class when_matching_single_string_that_should_match
    {
        const string string_to_match = "TheString";
        static FixedStringSegment segment;
        static ISegmentMatch result;

        Establish context = () => segment = new FixedStringSegment(string_to_match, false, SegmentOccurrence.Single, new NullSegment(), new ISegment[0]);

        Because of = () => result = segment.Match(new[] { string_to_match });

        It should_consider_it_a_match = () => result.HasMatch.ShouldBeTrue();
        It sould_hold_the_string_as_identifier = () => result.Identifier.ShouldEqual(string_to_match);
        It should_refer_to_segment_matched = () => result.Source.ShouldEqual(segment);
        It should_hold_the_string_to_match_as_only_value = () => result.Values.Single().ShouldEqual(string_to_match);
    }
}
