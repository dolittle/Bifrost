using System.Xml.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Configuration.Xml.for_ConfigParser
{
    public class when_config_object_has_a_config_element_with_a_string_property_and_it_is_set_as_an_inner_text_on_element : given.a_config_parser
    {
        static XDocument document;
        static ConfigObjectWithConfigObjectWithString config;
        static string expected_string;

        Establish context = () =>
                                {
                                    expected_string = "Something";
                                    document = XDocument.Parse(
                                        "<ConfigObjectWithConfigObjectWithString>"+
                                        "<Object>"+
                                        "<ConfigObjectWithStringProperty>"+
                                        "<Something>" + expected_string + "</Something>"+
                                        "</ConfigObjectWithStringProperty>"+
                                        "</Object>"+
                                        "</ConfigObjectWithConfigObjectWithString>");
                                };

        Because of = () => config = parser.Parse<ConfigObjectWithConfigObjectWithString>(document);

        It should_set_the_object_property = () => config.Object.ShouldNotBeNull();
        It should_set_property_the_same_as_inner_text = () => config.Object.Something.ShouldEqual(expected_string);
        
    }
}