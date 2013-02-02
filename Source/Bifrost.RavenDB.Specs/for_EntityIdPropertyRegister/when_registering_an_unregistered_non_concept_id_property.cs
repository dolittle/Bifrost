using System.Linq;
using System.Reflection;
using Bifrost.Testing.Fakes.Entities;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_EntityIdPropertyRegister
{
    [Subject(typeof(EntityIdPropertyRegister))]
    public class when_registering_an_unregistered_non_concept_id_property : given.an_entity_id_property_register_with_ids_registered
    {
        static bool is_id_property_before_registration;
        static bool is_id_property_after_registration;
        static PropertyInfo property_to_register;

        Establish context = () =>
                                {
                                    property_to_register = typeof(AnotherSimpleEntity).GetProperty("SimpleStringProperty");
                                };

        Because of = () =>
                         {
                             is_id_property_before_registration = id_property_register.IsIdProperty(typeof(AnotherSimpleEntity), property_to_register);
                             id_property_register.RegisterIdProperty<AnotherSimpleEntity, string>(e => e.SimpleStringProperty);
                             is_id_property_after_registration = id_property_register.IsIdProperty(typeof(AnotherSimpleEntity), property_to_register);
                         };

        It should_not_be_the_id_before_registration = () => is_id_property_before_registration.ShouldBeFalse();
        It should_be_the_id_after_registration = () => is_id_property_after_registration.ShouldBeTrue();
        It should_not_create_a_concept_type_converter_for_this = () => id_property_register.GetTypeConvertersForConceptIds().Count().ShouldEqual(1);
    }
}