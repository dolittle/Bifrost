using System.Linq;
using System.Reflection;
using Bifrost.Testing.Fakes.Concepts;
using Bifrost.Testing.Fakes.Entities;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_EntityIdPropertyRegister
{
    [Subject(typeof(EntityIdPropertyRegister))]
    public class when_registering_an_unregistered_concept_id_property : given.an_entity_id_property_register_with_ids_registered
    {
        static bool is_id_property_before_registration;
        static bool is_id_property_after_registration;
        static PropertyInfo property_to_register;

        Establish context = () =>
                                {
                                    property_to_register = typeof(AnotherSimpleEntity).GetProperty("TheIdProperty");
                                };

        Because of = () =>
                         {
                             is_id_property_before_registration = id_property_register.IsIdProperty(typeof(AnotherSimpleEntity), property_to_register);
                             id_property_register.RegisterIdProperty<AnotherSimpleEntity, ConceptAsLong>(e => e.TheIdProperty);
                             is_id_property_after_registration = id_property_register.IsIdProperty(typeof(AnotherSimpleEntity), property_to_register);
                         };

        It should_not_be_the_id_before_registration = () => is_id_property_before_registration.ShouldBeFalse();
        It should_be_the_id_after_registration = () => is_id_property_after_registration.ShouldBeTrue();
        It should_create_a_concept_type_converter_for_this_concept = ()=> id_property_register.GetTypeConvertersForConceptIds().Last()
                                                                                                                                .ShouldBeOfType<ConceptTypeConverter<ConceptAsLong, long>>();
    }
}