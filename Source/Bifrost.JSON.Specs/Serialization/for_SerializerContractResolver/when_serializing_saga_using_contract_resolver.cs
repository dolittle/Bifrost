using System.IO;
using System.Linq;
using Bifrost.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;
using Newtonsoft.Json;

namespace Bifrost.JSON.Specs.Serialization.for_SerializerContractResolver
{
    public class when_serializing_saga_using_contract_resolver : given.a_serializer_contract_resolver_that_ignores_saga_properties
    {
        static string[] saga_properties;
        static SimpleSaga saga;
        static string saga_as_json;


        Establish context = () =>
                                {
                                    saga = new SimpleSaga
                                               {
                                                   SomeString = "Something",
                                                SomeInt = 42,
                                                SomeDouble = 42.42d
                                               };
                                    saga_properties = typeof (ISaga).GetProperties().Select(t => t.Name).ToArray();
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


        It should_ignore_base_saga_properties = () => saga_properties.Any(saga_as_json.Contains).ShouldBeFalse();
        It should_include_string_property_from_implementation = () => saga_as_json.ShouldContain("SomeString");
        It should_include_int_property_from_implementation = () => saga_as_json.ShouldContain("SomeInt");
        It should_include_double_property_from_implementation = () => saga_as_json.ShouldContain("SomeDouble");
    }
}
