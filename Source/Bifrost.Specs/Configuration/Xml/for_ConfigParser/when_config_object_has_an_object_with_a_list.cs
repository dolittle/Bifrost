using System.Linq;
using System.Xml.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Configuration.Xml.for_ConfigParser
{
	public class when_config_object_has_an_object_with_a_list : given.a_config_parser
	{
		static XDocument document;
		static ConfigObjectWithListOfElements config;
		static string expected_string;

		Establish context = () =>
		                    	{
		                    		expected_string = "Something";
		                    		document = XDocument.Parse(
		                    			"<ConfigObjectWithListOfElements>"+
		                    			"<Elements>"+
		                    			"<ConfigObjectWithConfigObjectWithString>"+
		                    			"<Object>"+
		                    			"<ConfigObjectWithStringProperty Something=\""+expected_string+"\"/>"+
		                    			"</Object>" +
		                    			"</ConfigObjectWithConfigObjectWithString>" +
		                    			"</Elements>"+
		                    			"</ConfigObjectWithListOfElements>"
		                    			);
		                    	};

		Because of = () => config = parser.Parse<ConfigObjectWithListOfElements>(document);

		It should_have_the_elements_set = () => config.Elements.ShouldNotBeNull();
		It should_have_elements = () => config.Elements.ShouldNotBeEmpty();
		It should_have_inner_object_set = () => config.Elements.First().Object.ShouldNotBeNull();
		It should_have_string_set_for_inner_object = () => config.Elements.First().Object.Something.ShouldEqual(expected_string);
	}
}