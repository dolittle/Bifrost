using System.Collections.Generic;
using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Strings.for_VariableStringSegment
{
    public class when_matching_three_strings_with_matching_recurrance_with_child_segments
    {
        const string variable_name = "TheVariable";
        const string first_string_to_match = "FirstString";
        const string second_string_to_match = "SecondString";
        const string third_string_to_match = "ThirdString";

        static Mock<ISegment> first_child_segment_mock;
        static Mock<ISegment> second_child_segment_mock;

        static VariableStringSegment segment;
        static ISegmentMatch result;

        Establish context = () =>
        {
            first_child_segment_mock = new Mock<ISegment>();
            second_child_segment_mock = new Mock<ISegment>();
            segment = new VariableStringSegment(variable_name, false, SegmentOccurrence.Recurring, new NullSegment(), new ISegment[] { first_child_segment_mock.Object, second_child_segment_mock.Object });
        };

        Because of = () => result = segment.Match(new[] { first_string_to_match, second_string_to_match, third_string_to_match });

        It should_consider_it_a_match = () => result.HasMatch.ShouldBeTrue();
        It should_hold_the_variable_name_as_identifier = () => result.Identifier.ShouldEqual(variable_name);
        It should_have_three_values = () => result.Values.Count().ShouldEqual(3);
        It should_not_match_the_first_child = () => first_child_segment_mock.Verify(c => c.Match(Moq.It.IsAny<IEnumerable<string>>()), Times.Never());
        It should_not_match_the_second_child = () => second_child_segment_mock.Verify(c => c.Match(Moq.It.IsAny<IEnumerable<string>>()), Times.Never());
        It should_have_no_children = () => result.Children.Count().ShouldEqual(0);
    }
}
