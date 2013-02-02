using System;
using Bifrost.JSON.Serialization;
using Machine.Specifications;

namespace Bifrost.JSON.Specs.Serialization.for_ConceptConverter
{
    [Subject(typeof(ConceptConverter))]
    public class when_checking_can_convert_on_a_non_concept : given.a_concept_converter
    {
        static bool can_convert;

        Because of = () => can_convert = converter.CanConvert(typeof(Guid));

        It should_not_be_able_to_convert = () => can_convert.ShouldBeFalse();
    }
}