using System.Collections.Generic;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Strings.for_SegmentMatchesExtensions
{
    public class when_converting_matches_with_three_segment_matches
    {
        const string first_segment_identifier = "FirstSegment";
        const string second_segment_identifier = "SecondSegment";
        const string third_segment_identifier = "ThirdSegment";

        const string first_segment_value = "FirstValue";
        const string second_segment_value = "SecondValue";
        const string third_segment_first_value = "ThirdSegmentFirstValue";
        const string third_segment_second_value = "ThirdSegmentSecondValue";

        static Mock<ISegmentMatches> segment_matches;
        static Mock<ISegmentMatch> first_segment_match;
        static Mock<ISegmentMatch> second_segment_match;
        static Mock<ISegmentMatch> third_segment_match;

        static IDictionary<string, IEnumerable<string>> result;

        Establish context = () =>
        {
            first_segment_match = new Mock<ISegmentMatch>();
            second_segment_match = new Mock<ISegmentMatch>();
            third_segment_match = new Mock<ISegmentMatch>();

            first_segment_match.Setup(f => f.Identifier).Returns(first_segment_identifier);
            second_segment_match.Setup(f => f.Identifier).Returns(second_segment_identifier);
            third_segment_match.Setup(f => f.Identifier).Returns(third_segment_identifier);

            first_segment_match.Setup(f => f.Values).Returns(new[] { first_segment_value });
            second_segment_match.Setup(f => f.Values).Returns(new[] { second_segment_value });
            third_segment_match.Setup(f => f.Values).Returns(new[] { third_segment_first_value, third_segment_second_value });

            var matches = new List<ISegmentMatch>(new []
            {
                first_segment_match.Object,
                second_segment_match.Object,
                third_segment_match.Object
            });

            segment_matches = new Mock<ISegmentMatches>();
            segment_matches.Setup(s => s.GetEnumerator()).Returns(matches.GetEnumerator());
        };

        Because of = () => result = segment_matches.Object.AsDictionary();

        It should_hold_the_first_segment_identifier = () => result.Keys.ShouldContain(first_segment_identifier);
        It should_hold_the_second_segment_identifier = () => result.Keys.ShouldContain(second_segment_identifier);
        It should_hold_the_third_segment_identifier = () => result.Keys.ShouldContain(third_segment_identifier);
        It should_hold_value_for_the_first_segment = () => result[first_segment_identifier].ShouldContainOnly(first_segment_value);
        It should_hold_value_for_the_second_segment = () => result[second_segment_identifier].ShouldContainOnly(second_segment_value);
        It should_hold_values_for_the_third_segment = () => result[third_segment_identifier].ShouldContainOnly(third_segment_first_value, third_segment_second_value);
    }
}
