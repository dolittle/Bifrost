using System;
using Bifrost.Concepts;
using Bifrost.Extensions;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_TypeExtensions
{
    public class when_asking_what_concept_type_implements
    {
        static bool concept_implements_concept_as_guid;
        static bool concept_implements_generic_concept;
        static bool concept_implements_validatable;
        static bool concept_implements_equatable_of_concept_as_guid;
        static bool concept_implements_equatable;
        static bool concept_implements_object;

        Because of = () =>
        {
            concept_implements_concept_as_guid = typeof (ConceptType).Implements(typeof (ConceptAs<Guid>));
            concept_implements_generic_concept = typeof(ConceptType).Implements(typeof(ConceptAs<>));
            concept_implements_validatable = typeof (ConceptType).Implements(typeof (IAmValidatable));
            concept_implements_equatable_of_concept_as_guid = typeof (ConceptType).Implements(typeof (IEquatable<ConceptAs<Guid>>));
            concept_implements_equatable = typeof(ConceptType).Implements(typeof(IEquatable<>));
            concept_implements_object = typeof (ConceptType).Implements(typeof (Object));
        };

        It should_implement_concept_as_guid = () => concept_implements_concept_as_guid.ShouldBeTrue();

        It should_implement_generic_concept = () => concept_implements_generic_concept.ShouldBeTrue();

        It should_implement_validatable = () => concept_implements_validatable.ShouldBeTrue();

        It should_implement_equatable_of_concept_as_guid = () => concept_implements_equatable_of_concept_as_guid.ShouldBeTrue();

        It should_implement_equatable = () => concept_implements_equatable.ShouldBeTrue();

        // Maybe not?
        It should_implement_object = () => concept_implements_object.ShouldBeTrue();
    }
}