using System;
using System.IO;
using System.Linq;
using Bifrost.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;
using Newtonsoft.Json;

namespace Bifrost.JSON.Specs.Serialization.for_SerializerContractResolver
{
    public class when_serializing_saga_containing_properties_with_conflicting_names_using_contract_resolver: given.a_serializer_contract_resolver_that_ignores_saga_properties
    {
        static string[] saga_properties;
        static SimpleSagaWithConflictingPropertyNames saga;
        static string saga_as_json;

        static Guid saga_id = Guid.NewGuid();
        static Guid conflicting_id = Guid.NewGuid();



        Establish context = () =>
                                {
                                    saga = new SimpleSagaWithConflictingPropertyNames
                                               {
                                                   Id = saga_id,
                                                   SomeOtherThing = new SomeOtherThing() {Id = conflicting_id.ToString()}
                                               };
                                    saga_properties = typeof(ISaga).GetProperties().Select(t => t.Name).ToArray();
                                };

        Because of = () =>
                         {
                             using (var stringWriter = new StringWriter())
                             {
                                 var serializer = new JsonSerializer
                                                      {
                                                          TypeNameHandling = TypeNameHandling.Auto,
                                                          ContractResolver = contract_resolver
                                                      };
                                 serializer.Serialize(stringWriter, saga);
                                 saga_as_json = stringWriter.ToString();
                             }
                         };

        It should_ignore_non_conflicting_properties = () => saga_properties.Where(s => s != "Id").Any(saga_as_json.Contains).ShouldBeFalse();
        It should_ignore_base_conflicting_property_and_include_on_child = () => saga_properties.Where(s => s == "Id").Any(saga_as_json.Contains).ShouldBeTrue();
        It should_not_include_guid_property_from_isaga = () => saga_as_json.ShouldContain(conflicting_id.ToString());
        It should_include_string_property_from_implementation = () => saga_as_json.ShouldContain(conflicting_id.ToString());
    }
}