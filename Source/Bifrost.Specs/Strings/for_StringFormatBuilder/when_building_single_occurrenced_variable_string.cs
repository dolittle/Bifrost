using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatBuilder
{
    public class when_building_single_occurrenced_variable_string : given.an_empty_string_format_builder
    {
        const string variable_string = "Fixed String";
        static IStringFormatBuilder resulting_builder;
        static IStringFormat string_format;

        Establish context = () => resulting_builder = builder.VariableString(variable_string);

        Because of = () => string_format = resulting_builder.Build();

        It should_only_have_one_segment = () => string_format.Segments.Count().ShouldEqual(1);
        It should_hold_a_string_segment = () => string_format.Segments.First().ShouldBeOfExactType<VariableStringSegment>();
        It should_hold_the_fixed_string_on_segment = () => ((VariableStringSegment) string_format.Segments.First()).VariableName.ShouldEqual(variable_string);
    }
}
