using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Strings.for_StringFormat
{
    public class when_matching_three_strings_with_one_segment_with_two_children_that_should_match
    {
        const string first_string = "First";
        const string second_string = "Second";
        const string third_string = "Third";
        static string full_string = $"{first_string}.{second_string}.{third_string}";
        static StringFormat string_format;
        static ISegmentMatches matches;

        static Mock<ISegment> top_level_segment;
        static Mock<ISegmentMatch> top_level_match;
        static Mock<ISegmentMatch> first_child_segment_match;
        static Mock<ISegmentMatch> second_child_segment_match;

        Establish context = () =>
        {
            first_child_segment_match = new Mock<ISegmentMatch>();
            second_child_segment_match = new Mock<ISegmentMatch>();

            top_level_segment = new Mock<ISegment>();
            top_level_match = new Mock<ISegmentMatch>();
            top_level_match.Setup(t => t.HasMatch).Returns(true);
            top_level_match.Setup(t => t.Children).Returns(new[] { first_child_segment_match.Object, second_child_segment_match.Object });

            top_level_segment.Setup(t => 
                t.Match(new[] { first_string, second_string, third_string })).
                Returns(top_level_match.Object);
            
            string_format = new StringFormat(new ISegment[] { top_level_segment.Object }, '.');
        };

        Because of = () => matches = string_format.Match(full_string);

        It should_have_three_matches_in_right_order = () => matches.ShouldContainOnly(top_level_match.Object, first_child_segment_match.Object, second_child_segment_match.Object);
    }
}
