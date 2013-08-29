using System;
using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_ConceptExtensions
{
    [Subject(typeof(ConceptExtensions))]
    public class when_getting_the_value_from_a_non_concept : given.concepts
    {
        static string primitive_value = "ten";
        static object returned_value;
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => primitive_value.GetConceptValue());

        It should_throw_an_argument_exception = () => exception.ShouldBeOfType<ArgumentException>();
    }
}