using System;
using Bifrost.Concepts;
using Bifrost.Specs.Concepts.given;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptMap
{
    [Subject(typeof(ConceptMap))]
    public class when_getting_the_primitive_type_from_a_guid_concept
    {
        static Type result;

        Because of = () => result = ConceptMap.GetConceptValueType(typeof(concepts.GuidConcept));

        It should_get_a_guid = () => result.ShouldEqual(typeof(Guid));
    }
}