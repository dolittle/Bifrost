using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_ConceptTypeConverter
{
    [Subject(typeof(ConceptTypeConverter))]
    public class when_checking_can_convert_from_a_concept_of_string : given.a_concept_type_converter
    {
        static bool can_convert;

        Because of = () => can_convert = converter.CanConvertFrom(typeof(given.ConceptAsString));

        It should_be_able_to_convert = () => can_convert.ShouldBeTrue();
    }
}
