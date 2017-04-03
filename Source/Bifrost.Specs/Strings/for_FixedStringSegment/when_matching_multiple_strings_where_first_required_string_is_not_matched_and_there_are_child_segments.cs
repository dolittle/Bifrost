using System.Collections.Generic;
using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Strings.for_FixedStringSegment
{
    public class when_matching_multiple_strings_where_first_required_string_is_not_matched_and_there_are_child_segments
    {
        const string unmatched_string = "UnmatchedString";
        const string first_string_to_match = "FirstString";
        const string second_string_to_match = "SecondString";
        const string third_string_to_match = "ThirdString";
        static Mock<ISegment> first_child_segment_mock;
        static Mock<ISegment> second_child_segment_mock;
        static FixedStringSegment segment;
        static ISegmentMatch result;
        static Mock<ISegmentMatch> first_child_segment_match_mock;
        static Mock<ISegmentMatch> second_child_segment_match_mock;

        Establish context = () =>
        {
            first_child_segment_mock = new Mock<ISegment>();
            second_child_segment_mock = new Mock<ISegment>();

            first_child_segment_match_mock = new Mock<ISegmentMatch>();
            second_child_segment_match_mock = new Mock<ISegmentMatch>();

            first_child_segment_mock.Setup(f => f.Match(Moq.It.IsAny<IEnumerable<string>>())).Returns(first_child_segment_match_mock.Object);
            second_child_segment_mock.Setup(f => f.Match(Moq.It.IsAny<IEnumerable<string>>())).Returns(second_child_segment_match_mock.Object);

            segment = new FixedStringSegment(first_string_to_match, false, SegmentOccurrence.Single, new NullSegment(), new ISegment[] {
                first_child_segment_mock.Object,
                second_child_segment_mock.Object
            });
        };

        Because of = () => result = segment.Match(new[] { unmatched_string, second_string_to_match, third_string_to_match });

        It should_not_consider_it_a_match = () => result.HasMatch.ShouldBeFalse();
        It should_have_no_values = () => result.Values.Count().ShouldEqual(0);      
        It should_not_try_to_match_the_first_child = () => first_child_segment_mock.Verify(f => f.Match(Moq.It.IsAny<IEnumerable<string>>()), Times.Never);
        It should_not_try_to_match_the_second_child = () => first_child_segment_mock.Verify(f => f.Match(Moq.It.IsAny<IEnumerable<string>>()), Times.Never);
        It should_have_no_children = () => result.Children.Count().ShouldEqual(0);
    }
}
