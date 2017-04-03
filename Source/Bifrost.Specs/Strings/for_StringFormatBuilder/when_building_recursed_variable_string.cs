using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatBuilder
{
    public class when_building_recursed_variable_string : given.an_empty_string_format_builder
    {
        const string variable_string = "Variable String";
        static IStringFormatBuilder resulting_builder;
        static IStringFormat string_format;

        Establish context = () => resulting_builder = builder.VariableString(variable_string, f => f.Recurring());

        Because of = () => string_format = resulting_builder.Build();

        It should_hold_a_recursed_segment = () => ((VariableStringSegment) string_format.Segments.First()).Occurrences.ShouldEqual(SegmentOccurrence.Recurring);
    }
}
