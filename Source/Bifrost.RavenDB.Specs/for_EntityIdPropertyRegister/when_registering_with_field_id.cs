using System;
using System.Reflection;
using Bifrost.Testing.Fakes.Entities;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_EntityIdPropertyRegister
{
    [Subject(typeof(EntityIdPropertyRegister))]
    public class when_registering_with_field_id : given.an_entity_id_property_register_with_ids_registered
    {
        static Exception exception;
        static FieldInfo field_to_register;

        Establish context = () =>
                                {
                                    field_to_register = typeof(AnotherSimpleEntity).GetField("SimpleFieldValue");
                                };

        Because of = () => exception = Catch.Exception(() => id_property_register.RegisterIdProperty<AnotherSimpleEntity, string>(e => e.SimpleFieldValue));

        It should_not_allow_the_registration = () => exception.ShouldNotBeNull();
        It should_throw_an_invalid_id_exception = () => exception.ShouldBeOfExactType<InvalidIdException>();
    }
}