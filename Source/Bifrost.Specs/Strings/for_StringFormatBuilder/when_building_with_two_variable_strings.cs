using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatBuilder
{
    public class when_building_with_two_variable_strings : given.an_empty_string_format_builder
    {
        const string first_variable_string = "First Fixed String";
        const string second_variable_string = "Second Fixed String";
        static IStringFormatBuilder resulting_builder;
        static IStringFormat string_format;

        Establish context = () => resulting_builder = builder.VariableString(first_variable_string).VariableString(second_variable_string);

        Because of = () => string_format = resulting_builder.Build();

        It should_only_have_two_segment = () => string_format.Segments.Count().ShouldEqual(2);
        It should_hold_the_first_string_as_the_first_segment = () => ((VariableStringSegment) string_format.Segments.First()).VariableName.ShouldEqual(first_variable_string);
        It should_hold_the_second_string_as_the_second_segment = () => ((VariableStringSegment) string_format.Segments.ToArray()[1]).VariableName.ShouldEqual(second_variable_string);
        It should_point_to_the_first_string_as_parent_for_the_second_segment = () => ((VariableStringSegment) ((VariableStringSegment) string_format.Segments.ToArray()[1]).Parent).VariableName.ShouldEqual(((VariableStringSegment) string_format.Segments.First()).VariableName);
    }
}
