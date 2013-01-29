using Bifrost.Testing.Fakes.Concepts;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter
{
    [Subject(typeof(ConceptTypeConverter<,>))]
    public class when_checking_can_convert_from_a_concept_of_string : given.concept_converters
    {
        static bool guid_converter_can_convert;
        static bool long_converter_can_convert;
        static bool string_converter_can_convert;

        Because of = () =>
        {
            guid_converter_can_convert = converter_of_guid_concept.CanConvertFrom(typeof(ConceptAsString));
            long_converter_can_convert = converter_of_long_concept.CanConvertFrom(typeof(ConceptAsString));
            string_converter_can_convert = converter_of_string_concept.CanConvertFrom(typeof(ConceptAsString));
        };

        It should_not_be_convertable_by_guid_converter = () => guid_converter_can_convert.ShouldBeFalse();
        It should_not_be_convertable_by_string_converter = () => string_converter_can_convert.ShouldBeTrue();
        It should_be_convertable_by_long_converter = () => long_converter_can_convert.ShouldBeFalse();
    }
}
