using System;
using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptFactory
{
    [Subject(typeof(ConceptFactory))]
    public class when_creating_instance_of_datetime_concept_with_value_as_datetime
    {
        static DateTimeConcept result;
        static DateTime now;

        Establish context = () => now = DateTime.Now;

        Because of = () => result = ConceptFactory.CreateConceptInstance(typeof(DateTimeConcept), now) as DateTimeConcept;

        It should_be_the_value_of_the_datetime = () => result.Value.ShouldEqual(now);
    }
}