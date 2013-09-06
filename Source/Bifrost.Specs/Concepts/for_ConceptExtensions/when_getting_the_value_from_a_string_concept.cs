using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptExtensions
{
    [Subject(typeof(ConceptExtensions))]
    public class when_getting_the_value_from_a_string_concept : given.concepts
    {
        static StringConcept value;
        static string primitive_value = "ten";
        static object returned_value;

        Establish context = () =>
            {
                value = new StringConcept { Value = primitive_value };
            };

        Because of = () => returned_value = value.GetConceptValue();

        It should_get_the_value_of_the_primitive = () => returned_value.ShouldEqual(primitive_value);
    }
}