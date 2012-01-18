using System.Xml;
using System.Xml.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Configuration.Xml.for_ConfigParser
{
	public class when_object_has_config_object_inside_with_property_set_as_attribute_and_does_not_have_a_child_element_for_the_config_object : given.a_config_parser
	{
		static XDocument document;
		static ObjectWithConfigObjectInside config;
		static string expected_string;

		Establish context = () =>
		                    	{
		                    		expected_string = "Something";
		                    		document = XDocument.Parse
										("<ObjectWithConfigObjectInside><Object Something=\"" + expected_string + "\"/></ObjectWithConfigObjectInside>");
		                    	};

		Because of = () => config = parser.Parse<ObjectWithConfigObjectInside>(document);

		It should_set_the_config_object = () => config.Object.ShouldNotBeNull();
		It should_set_property_the_same_as_attribute = () => config.Object.Something.ShouldEqual(expected_string);
	}
}