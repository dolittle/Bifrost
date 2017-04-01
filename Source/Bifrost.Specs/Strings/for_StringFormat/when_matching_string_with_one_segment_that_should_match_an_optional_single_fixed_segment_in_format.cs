using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormat
{
    public class when_matching_string_with_one_segment_that_should_match_an_optional_single_fixed_segment_in_format
    {
        const string fixed_string = "FixedString";
        static StringFormat string_format;
        static ISegmentMatches matches;

        Establish context = () => 
            string_format = new StringFormat(
                new ISegment[] {
                    new FixedStringSegment(
                        fixed_string,
                        true,
                        SegmentOccurrence.Single,
                        new NullSegment(),
                        new ISegment[0])
                },'.');

        Because of = () => matches = string_format.Match(fixed_string);

        It should_have_one_match = () => matches.Count().ShouldEqual(1);
        It should_hold_a_segment_match_with_fixed_string = () => matches.First().Values.First().ShouldEqual(fixed_string);
    }
}
