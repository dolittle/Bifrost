using System.Xml.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Configuration.Xml.for_ConfigParser
{
    public class when_config_object_has_a_string_property_and_no_xml_setting_it : given.a_config_parser
    {
        static XDocument document;
        static ConfigObjectWithStringProperty config;

        Establish context = () =>
                                {
                                    document = XDocument.Parse("<ConfigObjectWithStringProperty/>");
                                };

        Because of = () => config = parser.Parse<ConfigObjectWithStringProperty>(document);

        It should_not_set_property = () => config.Something.ShouldBeNull();
    }
}