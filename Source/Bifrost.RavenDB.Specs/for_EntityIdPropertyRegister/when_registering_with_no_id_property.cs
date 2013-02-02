using System;
using Bifrost.Testing.Fakes.Concepts;
using Bifrost.Testing.Fakes.Entities;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_EntityIdPropertyRegister
{
    [Subject(typeof(EntityIdPropertyRegister))]
    public class when_registering_with_no_id_property : given.an_entity_id_property_register_with_ids_registered
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => id_property_register.RegisterIdProperty<SimpleEntity, ConceptAsGuid>(null));

        It should_not_allow_the_registration = () => exception.ShouldNotBeNull();
        It should_throw_an_invalid_id_exception = () => exception.ShouldBeOfType<InvalidIdException>();
    }
}