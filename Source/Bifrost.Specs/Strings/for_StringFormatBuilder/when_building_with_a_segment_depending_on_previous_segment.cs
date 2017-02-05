using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatBuilder
{
    public class when_building_with_a_segment_depending_on_previous_segment : given.an_empty_string_format_builder
    {
        const string first_fixed_string = "First Fixed String";
        const string second_fixed_string = "Second Fixed String";
        static IStringFormatBuilder resulting_builder;
        static IStringFormat string_format;

        Establish context = () => resulting_builder = builder.FixedString(first_fixed_string).FixedString(second_fixed_string, f => f.DependingOnPrevious());

        Because of = () => string_format = resulting_builder.Build();

        It should_only_have_one_top_level_segment = () => string_format.Segments.Count().ShouldEqual(1);
        It should_hold_the_second_string_as_a_child_of_the_first = () => ((FixedStringSegment) string_format.Segments.First().Children.Single()).String.ShouldEqual(second_fixed_string);
    }
}
