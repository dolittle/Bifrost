using System.Reflection;
using Bifrost.Testing.Fakes.Entities;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_EntityIdPropertyRegister
{
    [Subject(typeof(EntityIdPropertyRegister))]
    public class when_asking_if_the_registered_id_property_is_the_id_property : given.an_entity_id_property_register_with_ids_registered
    {
        static bool is_id_property;
        static PropertyInfo property_to_check;

        Establish context = () =>
                                {
                                    property_to_check = typeof (SimpleEntity).GetProperty("TheIdProperty");
                                };

        Because of = () => is_id_property = id_property_register.IsIdProperty(typeof(SimpleEntity), property_to_check);

        It should_be_the_id = () => is_id_property.ShouldBeTrue();
    }
}