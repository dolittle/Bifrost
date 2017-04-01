using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatBuilder
{
    public class when_building_with_two_fixed_strings : given.an_empty_string_format_builder
    {
        const string first_fixed_string = "First Fixed String";
        const string second_fixed_string = "Second Fixed String";
        static IStringFormatBuilder resulting_builder;
        static IStringFormat string_format;

        Establish context = () => resulting_builder = builder.FixedString(first_fixed_string).FixedString(second_fixed_string);

        Because of = () => string_format = resulting_builder.Build();

        It should_only_have_two_segment = () => string_format.Segments.Count().ShouldEqual(2);
        It should_hold_the_first_string_as_the_first_segment = () => ((FixedStringSegment)string_format.Segments.First()).String.ShouldEqual(first_fixed_string);
        It should_hold_the_second_string_as_the_second_segment = () => ((FixedStringSegment) string_format.Segments.ToArray()[1]).String.ShouldEqual(second_fixed_string);
        It should_point_to_the_first_string_as_parent_for_the_second_segment = () => ((FixedStringSegment)((FixedStringSegment) string_format.Segments.ToArray()[1]).Parent).String.ShouldEqual(((FixedStringSegment) string_format.Segments.First()).String);
    }
}
