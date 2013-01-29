using System;
using System.Reflection;
using Bifrost.Testing.Fakes.Concepts;
using Bifrost.Testing.Fakes.Entities;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_EntityIdPropertyRegister
{
    [Subject(typeof(EntityIdPropertyRegister))]
    public class when_registering_an_already_registered_id_property : given.an_entity_id_property_register_with_ids_registered
    {
        static Exception exception;
        static PropertyInfo property_to_register;

        Establish context = () =>
                                {
                                    property_to_register = typeof(SimpleEntity).GetProperty("TheIdProperty");
                                };

        Because of = () => exception = Catch.Exception(() => id_property_register.RegisterIdProperty<SimpleEntity, ConceptAsGuid>(e => e.TheIdProperty));

        It should_not_allow_the_registration = () => exception.ShouldNotBeNull();
        It should_throw_a_duplicate_id_exception = () => exception.ShouldBeOfType<DuplicateIdRegistrationForTypeException>();
    }
}