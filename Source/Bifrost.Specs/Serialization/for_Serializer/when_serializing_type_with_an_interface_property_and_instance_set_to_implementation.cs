using Machine.Specifications;

namespace Bifrost.Specs.Serialization.for_Serializer
{
	public class when_serializing_type_with_an_interface_property_and_instance_set_to_implementation : given.a_serializer
	{
		const string expected_content_value = "Something";

		static ClassToSerialize class_to_serialize;
		
		static string result;

		Establish context = () =>
		                    	{
									class_to_serialize = new ClassToSerialize();
									class_to_serialize.Something = new SomethingImplementation { SomeValue = expected_content_value };
		                    	};

		Because of = () => result = serializer.ToJson(class_to_serialize);

		It should_contain_type_information = () => result.ShouldContain(typeof (SomethingImplementation).Name);
	}
}
