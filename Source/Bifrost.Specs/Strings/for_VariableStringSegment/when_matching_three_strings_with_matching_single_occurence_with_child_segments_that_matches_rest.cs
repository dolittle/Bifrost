using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Strings.for_VariableStringSegment
{
    public class when_matching_three_strings_with_matching_single_occurence_with_child_segments_that_matches_rest
    {
        const string variable_name = "TheVariable";
        const string first_string_to_match = "FirstString";
        const string second_string_to_match = "SecondString";
        const string third_string_to_match = "ThirdString";

        static Mock<ISegment> first_child_segment_mock;
        static Mock<ISegmentMatch> first_child_segment_match_mock;

        static Mock<ISegment> second_child_segment_mock;
        static Mock<ISegmentMatch> second_child_segment_match_mock;

        static VariableStringSegment segment;
        static ISegmentMatch result;

        Establish context = () =>
        {
            first_child_segment_mock = new Mock<ISegment>();
            first_child_segment_match_mock = new Mock<ISegmentMatch>();
            first_child_segment_match_mock.Setup(c => c.HasMatch).Returns(true);
            first_child_segment_match_mock.Setup(c => c.Values).Returns(new[] { second_string_to_match });
            first_child_segment_mock.Setup(c => c.Match(new[] { second_string_to_match, third_string_to_match })).Returns(first_child_segment_match_mock.Object);

            second_child_segment_mock = new Mock<ISegment>();
            second_child_segment_match_mock = new Mock<ISegmentMatch>();
            second_child_segment_match_mock.Setup(c => c.HasMatch).Returns(true);
            second_child_segment_match_mock.Setup(c => c.Values).Returns(new[] { second_string_to_match });
            second_child_segment_mock.Setup(c => c.Match(new[] { third_string_to_match })).Returns(first_child_segment_match_mock.Object);

            segment = new VariableStringSegment(variable_name, false, SegmentOccurrence.Single, new NullSegment(), new ISegment[] { first_child_segment_mock.Object, second_child_segment_mock.Object });
        };

        Because of = () => result = segment.Match(new[] { first_string_to_match, second_string_to_match, third_string_to_match });

        It should_consider_it_a_match = () => result.HasMatch.ShouldBeTrue();
        It should_hold_the_variable_name_as_identifier = () => result.Identifier.ShouldEqual(variable_name);
        It should_have_one_value = () => result.Values.Count().ShouldEqual(1);
        It should_hold_the_first_string_as_value = () => result.Values.First().ShouldEqual(first_string_to_match);
        It should_match_the_last_two_strings_with_first_child = () => first_child_segment_mock.Verify(c => c.Match(new[] { second_string_to_match, third_string_to_match }), Times.Once());
        It should_match_the_last_string_with_second_child = () => second_child_segment_mock.Verify(c => c.Match(new[] { third_string_to_match }), Times.Once());
        It should_have_two_children = () => result.Children.Count().ShouldEqual(2);
    }
}
