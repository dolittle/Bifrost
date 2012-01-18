
using System.Xml.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Configuration.Xml.for_ConfigParser
{
    public class when_config_object_has_a_string_property_and_it_is_set_as_an_attribute : given.a_config_parser
    {
        static XDocument document;
        static ConfigObjectWithStringProperty config;
        static string expected_string;

        Establish context = () =>
                                {
                                    expected_string = "Something";
                                    document = XDocument.Parse("<ConfigObjectWithStringProperty Something=\""+expected_string+"\"/>");
                                };

        Because of = () => config = parser.Parse<ConfigObjectWithStringProperty>(document);

        It should_set_property_the_same_as_attribute = () => config.Something.ShouldEqual(expected_string);
    }
}