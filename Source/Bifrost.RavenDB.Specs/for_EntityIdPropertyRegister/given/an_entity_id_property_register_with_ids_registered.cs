using Bifrost.Testing.Fakes.Concepts;
using Bifrost.Testing.Fakes.Entities;
using Machine.Specifications;

namespace Bifrost.RavenDB.Specs.for_EntityIdPropertyRegister.given
{
    public class an_entity_id_property_register_with_ids_registered
    {
        protected static IEntityIdPropertyRegister id_property_register;
        
        Establish context = () =>
                                {
                                    id_property_register = new EntityIdPropertyRegister();
                                    id_property_register.RegisterIdProperty<SimpleEntity,ConceptAsGuid>(e => e.TheIdProperty);
                                };
    }
}