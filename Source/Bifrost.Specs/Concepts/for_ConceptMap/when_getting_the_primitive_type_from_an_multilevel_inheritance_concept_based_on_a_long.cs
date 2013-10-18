using System;
using Bifrost.Concepts;
using Bifrost.Specs.Concepts.given;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptMap
{
    [Subject(typeof(ConceptMap))]
    public class when_getting_the_primitive_type_from_an_multilevel_inheritance_concept_based_on_a_long
    {
        static Type result;

        Because of = () => result = ConceptMap.GetConceptValueType(typeof(concepts.MultiLevelInheritanceConcept));

        It should_get_a_long = () => result.ShouldEqual(typeof(long));
    }
}