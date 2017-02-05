using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatBuilder
{
    public class when_building_with_one_string_segment : given.an_empty_string_format_builder
    {
        const string fixed_string = "Fixed String";
        static IStringFormatBuilder resulting_builder;
        static IStringFormat string_format;

        Establish context = () => resulting_builder = builder.FixedString(fixed_string);

        Because of = () => string_format = resulting_builder.Build();

        It should_only_have_one_segment = () => string_format.Segments.Count().ShouldEqual(1);
        It should_hold_a_string_segment = () => string_format.Segments.First().ShouldBeOfExactType<FixedStringSegment>();
        It should_hold_the_fixed_string_on_segment = () => ((FixedStringSegment) string_format.Segments.First()).String.ShouldEqual(fixed_string);
    }
}
