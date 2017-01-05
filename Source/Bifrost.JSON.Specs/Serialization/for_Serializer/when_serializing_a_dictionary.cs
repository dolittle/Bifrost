using System.Collections.Generic;
using Bifrost.JSON.Serialization;
using Bifrost.Serialization;
using Machine.Specifications;

namespace Bifrost.JSON.Specs.Serialization.for_Serializer
{
    [Subject(typeof(Serializer))]
    public class when_serializing_a_dictionary : given.a_serializer
    {
        static IDictionary<string, string> to_serialize;
        static string serialized_version;
        static string serialized_version_camel_case;

        Establish context = () =>
        {
            to_serialize = new Dictionary<string, string>
            {
                {"Key1", "Value1"},
                {"Key2", "Value2"},
            };
        };

        Because of = () =>
        {
            serialized_version = serializer.ToJson(to_serialize);
            serialized_version_camel_case = serializer.ToJson(to_serialize, SerializationOptions.CamelCase);
        };

        It should_serialize_the_dictionary = () =>
            serialized_version.ShouldEqual("{\"Key1\":\"Value1\",\"Key2\":\"Value2\"}");

        It should_serialize_the_dictionary_in_camel_case = () =>
            serialized_version_camel_case.ShouldEqual("{\"key1\":\"Value1\",\"key2\":\"Value2\"}");
    }
}