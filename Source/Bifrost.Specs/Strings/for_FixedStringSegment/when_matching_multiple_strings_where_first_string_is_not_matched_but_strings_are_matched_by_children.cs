using System.Collections.Generic;
using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Strings.for_FixedStringSegment
{
    public class when_matching_multiple_strings_where_first_string_is_not_matched_but_strings_are_matched_by_children
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
            first_child_segment_match_mock.SetupGet(m => m.HasMatch).Returns(true);
            first_child_segment_match_mock.SetupGet(m => m.Values).Returns(new[] { first_string_to_match });
            second_child_segment_match_mock = new Mock<ISegmentMatch>();
            second_child_segment_match_mock.SetupGet(m => m.HasMatch).Returns(true);
            second_child_segment_match_mock.SetupGet(m => m.Values).Returns(new[] { second_string_to_match });

            first_child_segment_mock.Setup(f => f.Match(new[] { first_string_to_match, second_string_to_match, third_string_to_match }));
            first_child_segment_mock.Setup(f => f.Match(new[] { second_string_to_match, third_string_to_match }));

            first_child_segment_mock.Setup(f => f.Match(Moq.It.IsAny<IEnumerable<string>>())).Returns(first_child_segment_match_mock.Object);
            second_child_segment_mock.Setup(f => f.Match(Moq.It.IsAny<IEnumerable<string>>())).Returns(second_child_segment_match_mock.Object);

            segment = new FixedStringSegment(first_string_to_match, true, SegmentOccurrence.Single, new NullSegment(), new ISegment[] {
                first_child_segment_mock.Object,
                second_child_segment_mock.Object
            });
        };

        Because of = () => result = segment.Match(new[] { unmatched_string, first_string_to_match, second_string_to_match, third_string_to_match });

        It should_consider_it_a_match = () => result.HasMatch.ShouldBeTrue();
        It should_have_no_values = () => result.Values.Count().ShouldEqual(0);      
        It should_match_the_three_last_strings_with_the_first_child = () => first_child_segment_mock.Verify(f => f.Match(new[] { unmatched_string, first_string_to_match, second_string_to_match, third_string_to_match }));
        It should_match_the_two_last_strings_with_the_second_child = () => second_child_segment_mock.Verify(f => f.Match(new[] { first_string_to_match, second_string_to_match, third_string_to_match }));
        It should_have_two_children = () => result.Children.Count().ShouldEqual(2);
    }
}
